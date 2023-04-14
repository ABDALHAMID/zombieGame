using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyControle : MonoBehaviour
{
    private NavMeshAgent _meshAgent;
    [SerializeField] private GameObject _enemyGraphics;
    private GameObject player;
    private EnemyAttacks enemyAttack;
    private HealthSystem healthSystem;
    [SerializeField] private LayerMask _playerLayer;
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
    public int numberOfXRaycasts = 10;
    public int numberOfYRaycasts = 4;
    public float raycastYAngle = 20f, raycastXAngle = 10f, raycastLength = 100f, timeToStopChasing = 5f;
    private bool isChasing = false, chasingInvoke = false;
    [Range(0f, 1f)] public float _rayLimitRotation;

    //Attacking
    private bool isAttacking, attackOneTime = true;

    //States
    public float attackRange = 2;
    private int fraction = 1;
    private bool readyToStopChasing = false, seePlayer = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _meshAgent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttacks>();
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Update()
    {
        //alwase set the graphics of the enemy in
        //_enemyGraphics.transform.SetPositionAndRotation(transform.position, transform.rotation);

        if (!isAttacking)
        {
            if (_meshAgent != null) _meshAgent.isStopped = false;

            //check if the enemey not is a position to attack the player or chasing him to start the raycasting to check if he see the player
            if (!isChasing) 
            {
                for (int j = 0; j < numberOfYRaycasts; j++)
                {
                    for (int i = 0; i < numberOfXRaycasts; i++)
                    {
                        // Create a ray in the forward direction of the zombie
                        Vector3 raycastDirection = scherchPoint.forward;
                        // Rotate the raycast direction based on the angle and number of raycasts
                        raycastDirection = Quaternion.AngleAxis((i - numberOfXRaycasts / 2) * raycastYAngle, scherchPoint.up) * raycastDirection;
                        raycastDirection = Quaternion.AngleAxis((j - numberOfYRaycasts / 2) * raycastXAngle, scherchPoint.right) * raycastDirection;
                        Ray ray = new(scherchPoint.position, raycastDirection);

                        // Perform the raycast
                        if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
                        {
                            // If the raycast hits the player, start navigating to the player
                            if (hit.collider != null && hit.collider.CompareTag(player.tag.ToString())) ChasePlayer();
                            else seePlayer = false;
                        }
                    }
                }
            }
            else
            {
                if (chasingInvoke)
                {
                    chasingInvoke = false;
                    Invoke(nameof(StopChasing), timeToStopChasing);
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
            if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), attackRange, _playerLayer) && attackOneTime)
            {
                attackOneTime = false;
                Debug.Log("attack");
                AttackPlayer();
            }
        }
        //if the enemey die remove this script
        if (healthSystem.GetIsDead())
        {
            _meshAgent.isStopped = true;
            Destroy(_meshAgent);
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            if (isChasing) _meshAgent.SetDestination(player.transform.position);
            else Patroling();
        }
    }
    private void StopChasing()
    {
        readyToStopChasing = true;
        chasingInvoke = true;
    }
    private void Patroling()
    {
        if (!walkPointSet) BeIdel();

        if (walkPointSet)
        {
            _meshAgent.SetDestination(walkPoint);
            isIdel = false;
            _meshAgent.speed = walkingSpeed;
            curantState = 2;
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f) 
            { 
                _meshAgent.SetDestination(transform.position);
                walkPointSet = false;
            }
        }
    }
    private void BeIdel()
    {
        if (!isIdel)
        {
            isIdel = true;
            curantState = 1;
            Invoke(nameof(SearchWalkPoint), Random.Range(2f, 7f));
        }
    }
    private void SearchWalkPoint()
    {
        walkPoint = new Vector3(transform.position.x + Random.Range(-walkPointRange, walkPointRange),
                                transform.position.y,
                                transform.position.z + Random.Range(-walkPointRange, walkPointRange));
        if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, walkPointRange, _meshAgent.areaMask))
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
        _meshAgent.speed = runSpeed;
        curantState = 3;
    }
    

    private void AttackPlayer()
    {
        _meshAgent.isStopped = true;
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        Debug.Log("Attack Player function");
        enemyAttack.Attack();
    }
    public void SetIsAttacking(bool value)
    {
        isAttacking = value;
    }
    public int GetCurantState()
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
        for (int j = 0; j < numberOfYRaycasts; j++)
        {
            for (int i = 0; i < numberOfXRaycasts; i++)
            {
                Vector3 raycastDirection = scherchPoint.forward;
                raycastDirection = Quaternion.AngleAxis((i - numberOfXRaycasts / 2) * raycastYAngle, scherchPoint.up) * raycastDirection;
                raycastDirection = Quaternion.AngleAxis((j - numberOfYRaycasts / 2) * raycastXAngle, scherchPoint.right) * raycastDirection;
                Ray ray = new(scherchPoint.position, raycastDirection);
                Gizmos.DrawRay(ray);
            }
        }
    }

}
