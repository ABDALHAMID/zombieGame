using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyControle : MonoBehaviour
{
    public float speed = 5f; // The speed at which the enemy moves
    public float chaseRange = 30f; // The distance at which the enemy will start chasing the player
    public float attackRange = 5f; // The distance at which the enemy will attack the player
    public float patrolRadius = 10f; // The radius within which the enemy will patrol
    public Transform[] patrolPoints; // The points that the enemy will patrol between
    private Transform player; // The player's transform
    private int currentPatrolPoint = 0; // The current patrol point that the enemy is heading towards
    private bool isChasing = false; // Whether the enemy is currently chasing the player
    private bool isAttacking = false; // Whether the enemy is currently attacking the player

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the player's transform and the enemy's rigidbody
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If the player is within the chase range and is not already chasing or attacking, start chasing the player
        if (distanceToPlayer <= chaseRange && !isChasing && !isAttacking)
        {
            StartChasing();
        }
        // If the player is outside the chase range and is currently chasing, stop chasing and start patrolling
        else if (distanceToPlayer > chaseRange && isChasing)
        {
            StopChasing();
        }
        // If the player is within the attack range and is currently chasing, start attacking the player
        else if (distanceToPlayer <= attackRange && isChasing)
        {
            Attack();
        }

        // If the enemy is currently chasing the player, move towards the player's position
        if (isChasing)
        {
            MoveTowardsPlayer();
        }
        // If the enemy is not currently chasing the player, move towards the next patrol point
        else
        {
            MoveTowardsPatrolPoint();
        }
    }

    // Function to start chasing the player
    void StartChasing()
    {
        isChasing = true;
    }

    // Function to stop chasing the player and start patrolling
    void StopChasing()
    {
        isChasing = false;
    }

    // Function to start attacking the player
    void Attack()
    {
        isAttacking = true;
    }

    // Function to move towards the player's position
    void MoveTowardsPlayer()
    {
    // Translate the enemy in that direction
    transform.Translate(transform.forward * speed * Time.deltaTime);

        // Set the enemy's rotation to face the player
        transform.LookAt(player);
        /*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    transform.rotation = Quaternion.Euler(0f, 0f, angle);*/
        
    }

    //set un partol point for the enemy to move at it
    void SetPartolPoint()
    {
        patrolPoints[currentPatrolPoint].transform.position = new Vector3(transform.position.x + Random.Range(-5f, 5f),
                                                         transform.position.y,
                                                         transform.position.z + Random.Range(-5f, 5f));
        
    }

    // Function to move towards the next patrol point
    void MoveTowardsPatrolPoint()
    {
        //walk in the forward dirction
        transform.Translate(transform.forward * speed * Time.deltaTime);
        //

        /*
                // Calculate the distance to the patrol point
                float distanceToPatrolPoint = Vector2.Distance(transform.position, patrolPoints[currentPatrolPoint].position);

                // If the enemy is within the patrol radius, move towards the patrol point
                if (distanceToPatrolPoint <= patrolRadius)
                {

                    // Translate the enemy in that direction
                    transform.Translate(transform.forward * speed * Time.deltaTime);

                    // Set the enemy's rotation to face the patrol point
                    transform.LookAt(patrolPoints[currentPatrolPoint]);
                }

                // If the enemy has reached the patrol point, select the next patrol point
                if (distanceToPatrolPoint < 0.2f)
                {
                    SetPartolPoint();
                }*/
    }

    // Function called when the enemy collides with a wall
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy has collided with a wall, stop chasing or patrolling and turn around
        if (collision.gameObject.tag == "Wall")
        {
            if (isChasing)
            {
                StopChasing();
            }
            else
            {
                currentPatrolPoint--;
                if (currentPatrolPoint < 0)
                {
                    currentPatrolPoint = patrolPoints.Length - 1;
                }
            }
            transform.Rotate(0f, 180f, 0f);
        }
    }

    // Function called when the enemy finishes attacking
    void OnAttackFinished()
    {
        isAttacking = false;
    }


}
