using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyControle : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform player;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange = 7f;

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
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        inPositionToAttack = true;
    }
    public bool getInPositionToAttacke()
    {
        return inPositionToAttack;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
