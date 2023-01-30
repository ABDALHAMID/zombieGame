using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyControle : MonoBehaviour
{
    private NavMeshAgent agent;

    private GameObject Player;
    private Transform player;

    //idel
    private bool isIdel;

    /*a variable define the state of the enemey 
    1 >> idle;
    2 >> wallking;
    3 >> chassing;
    4 >> attaking;*/
    private int curantState;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange = 12f;
    public float walkingSpeed = 1f;
    public LayerMask ground;

    //chasing
    public float runSpeed = 3f;
    public Transform scherchPoint;
    Ray ray;
    public int numberOfRaycasts = 10;
    public float raycastAngle = 20f, raycastLength = 100f, timeToStopChasing = 5f;
    private bool isChasing = false, chasingInvoke = false;

    //Attacking
    private bool inPositionToAttack;

    //States
    public float chaseRange = 15, attackRange = 2;
    private float distanceToPlayer;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (isChasing) ChasePlayer();
        for (int i = 0; i < numberOfRaycasts; i++)
        {
            // Create a ray in the forward direction of the zombie
            Vector3 raycastDirection = scherchPoint.forward;
            // Rotate the raycast direction based on the angle and number of raycasts
            raycastDirection = Quaternion.AngleAxis((i - numberOfRaycasts / 2) * raycastAngle, scherchPoint.up) * raycastDirection;
            Ray ray = new Ray(scherchPoint.position, raycastDirection);
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            // Perform the raycast
            if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
            {
                // If the raycast hits the player, start navigating to the player
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    if (distanceToPlayer < attackRange) AttackPlayer();
                    else ChasePlayer();
                }
                else
                {
                    Patroling();
                }

            }
            else
            {
                Patroling();
            }
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) beIdel();

        if (walkPointSet)
        {
            isIdel = false;
            agent.SetDestination(walkPoint);
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
        if (NavMesh.SamplePosition(walkPoint, out hit, 1f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
        else
        {
            SearchWalkPoint();
        }
        Debug.DrawRay(walkPoint, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
    }
  
    
    private void ChasePlayer()
    {
        isChasing = true;
        if (chasingInvoke)
        {
            chasingInvoke = false;
            Invoke("StopChasing", timeToStopChasing);
        }
        agent.SetDestination(player.position);
        agent.speed = runSpeed;
        curantState = 3;
    }
    private void StopChasing()
    {
        isChasing = false;
        chasingInvoke = true;
    }

    private void AttackPlayer()
    {
        agent.isStopped = true;
        inPositionToAttack = true;
        curantState = 4;
    }
    public bool getInPositionToAttacke()
    {
        return inPositionToAttack;
    }
    public int getCurantState()
    {
        return curantState;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
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
