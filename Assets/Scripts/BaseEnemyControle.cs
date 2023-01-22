using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyControle : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform player;

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

    //chasing
    public float runSpeed = 3f;

    //Attacking
    private bool inPositionToAttack;

    //States
    public float chaseRange = 15, attackRange = 2;
    private float distanceToPlayer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //calculate the distance betwen the enemy and the player
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //check wath should the enemy do attack or chase or patroling
        if (distanceToPlayer < attackRange) AttackPlayer();
        else inPositionToAttack = false;
        if (distanceToPlayer < chaseRange) ChasePlayer();
        else    Patroling();
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            
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
    private void SearchWalkPoint()
    {
        walkPoint = new Vector3(transform.position.x + Random.Range(-walkPointRange, walkPointRange),
                                transform.position.y,
                                transform.position.z + Random.Range(-walkPointRange, walkPointRange));

        walkPointSet = true;
    }
  
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = runSpeed;
        curantState = 3;
    }

    private void AttackPlayer()
    {
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
    }
}
