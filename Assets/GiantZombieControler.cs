using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class GiantZombieControler : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;
    public LayerMask _playerLayer;
    private bool start;
    private bool Attacking;
    private string[] animations = {"run", "roaring", "jump", "swiping" };
    public float _swipingCheckRadius, _jumpChechRadius;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

    }
    public void OnGiantZombieStart()
    {
        start = true;
        Debug.Log("start");
    }
    private void Update()
    {
        if (start)
        {
            if (agent != null) agent.isStopped = false;
            

            PlayAnimation("run");
            if (Physics.CheckSphere(transform.position, _jumpChechRadius, _playerLayer)) PlayAnimation("jump");
            if (Physics.CheckSphere(transform.position, _swipingCheckRadius, _playerLayer)) PlayAnimation("swiping");



        }
    }
    private void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);
    }
    private void PlayAnimation(string name)
    {
        foreach(string anim in animations)
        {
            if (anim == name) animator.SetBool(name, true);
            else animator.SetBool(name, false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _swipingCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _jumpChechRadius);
    }
}
