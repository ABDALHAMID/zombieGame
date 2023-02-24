using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyControle : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private GameObject enemyGraphics;
    private GameObject Player;
    private Transform player;
    private enemyAttacks enemyAttack;
    private healthSystem healthSystem;
    [SerializeField]
    private LayerMask playerLayer;
    public float rotationSpeed = 1.0f;

    //idel
    private bool isIdel;

    /*a variable define the state of the enemey 
    1 >> idle;
    2 >> wallking;
    3 >> chassing;
    4 >> attaking;*/
    private int curantState = 1;

    //Patroling
    private Vector3 walkPoint;
    private bool walkPointSet = false;
    public float walkPointRange = 12f;
    public float walkingSpeed = 1f;
    public LayerMask ground;

    //chasing
    public float runSpeed = 3f;
    public Transform scherchPoint;
    private Ray ray;
    public int numberOfRaycasts = 10;
    public float raycastAngle = 20f, raycastLength = 100f, timeToStopChasing = 5f;
    private bool isChasing = false, chasingInvoke = false;

    //Attacking
    private bool isAttacking, attackOneTime = true;

    //States
    public float attackRange = 2;
    private int fraction = 1;
    private bool readyToStopChasing = false, seePlayer = false;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<enemyAttacks>();
        healthSystem = GetComponent<healthSystem>();
    }
    private void Update()
    {
        enemyGraphics.transform.position = transform.position;
        enemyGraphics.transform.rotation = transform.rotation;

        if (!isAttacking)
        {
            Debug.Log("isAttacking = false");

            if (agent != null) agent.isStopped = false;
            if (scherchPoint.localRotation.x > 0.30f || scherchPoint.localRotation.x < -0.3 ) fraction *= -1;


            //check if the enemey not is a position to attack the player or chasing him to start the raycasting to check if he see the player
            if (!isChasing) 
            {
                for (int i = 0; i < numberOfRaycasts; i++)
                {
                    // Create a ray in the forward direction of the zombie
                    Vector3 raycastDirection = scherchPoint.forward;
                    // Rotate the raycast direction based on the angle and number of raycasts
                    raycastDirection = Quaternion.AngleAxis((i - numberOfRaycasts / 2) * raycastAngle, scherchPoint.up) * raycastDirection;
                    Ray ray = new Ray(scherchPoint.position, raycastDirection);

                    // Perform the raycast
                    if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
                    {
                        // If the raycast hits the player, start navigating to the player
                        if (hit.collider != null && hit.collider.CompareTag("Player")) ChasePlayer();
                        else seePlayer = false;
                    }

        
                }
            }
            else
            {
                if (chasingInvoke)
                {
                    chasingInvoke = false;
                    Invoke("StopChasing", timeToStopChasing);
                }
                if (readyToStopChasing && !seePlayer)
                {
                    isChasing = false;
                    readyToStopChasing = false;
                }
            }
            //set curantState
            if (isChasing) curantState = 3;
            else if (isIdel) curantState = 1;
            else curantState = 2;
            //check if the player in the attack range of the enemey
            if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), attackRange, playerLayer) && attackOneTime)
            {
                attackOneTime = false;
                Debug.Log("attack");
                AttackPlayer();
            }
        }
        //if the enemey die remove this script
        if (healthSystem.GetIsDead())
        {
            agent.isStopped = true;
            Destroy(agent);
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            if (isChasing) agent.SetDestination(player.position);
            else Patroling();
            //rotate the serchPoint so the zombie will see up and down
            scherchPoint.Rotate(new Vector3(fraction * .5f, 0, 0));
        }
    }
    private void StopChasing()
    {
        readyToStopChasing = true;
        chasingInvoke = true;
    }
    private void Patroling()
    {
        if (!walkPointSet) beIdel();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            isIdel = false;
            agent.speed = walkingSpeed;
            curantState = 2;
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f) 
            { 
                agent.SetDestination(transform.position);
                walkPointSet = false;
            }
        }
    }
    private void beIdel()
    {
        if (!isIdel)
        {
            isIdel = true;
            curantState = 1;
            Invoke("SearchWalkPoint", Random.Range(2f, 7f));
        }
    }
    private void SearchWalkPoint()
    {
        walkPoint = new Vector3(transform.position.x + Random.Range(-walkPointRange, walkPointRange),
                                transform.position.y,
                                transform.position.z + Random.Range(-walkPointRange, walkPointRange));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(walkPoint, out hit, walkPointRange, agent.areaMask))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
        else
        {
            SearchWalkPoint();
        }
        Debug.DrawRay(walkPoint, Vector3.up, Color.black); //so we can see with gizmos
    }
  
    
    private void ChasePlayer()
    {
        isChasing = true;
        agent.speed = runSpeed;
        curantState = 3;
    }
    

    private void AttackPlayer()
    {
        agent.isStopped = true;
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        Debug.Log("Attack Player function");
        enemyAttack.Attack();
    }
    public void SetIsAttacking(bool value)
    {
        isAttacking = value;
    }
    public int getCurantState()
    {
        return curantState;
    }
    public void SetAttackOneTime(bool value)
    {
        attackOneTime = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), attackRange);
        Gizmos.color = Color.blue;
        for (int i = 0; i < numberOfRaycasts; i++)
        {
            Vector3 raycastDirection = scherchPoint.forward;
            raycastDirection = Quaternion.AngleAxis((i - numberOfRaycasts / 2) * raycastAngle, scherchPoint.up) * raycastDirection;
            Ray ray = new Ray(scherchPoint.position, raycastDirection);
            Gizmos.DrawRay(ray);
        }
    }

}
