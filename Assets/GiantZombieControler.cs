using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class GiantZombieControler : MonoBehaviour
{
    [SerializeField] private List<EnemyAttackAnimation> _attackAnimations;
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;
    public LayerMask _playerLayer;
    public HealthSystem healthSystem;
    public bool start;
    public float _swipingCheckRadius, _jumpChechRadius;
    private bool isAttacking=false;
    [SerializeField] private AudioSource audioSource;
    public GiantZombieSound[] sounds;
    [SerializeField]
    private Vector3 attackPos;
    [SerializeField] private float rd;
    private float damage;
    public float attackSpeed;
    public float runSpeed;
    public SeconStageManeger stageManeger;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        PlaySound(GiantZombieSound.SoundType.idle);
        agent.speed = runSpeed;

    }
    public void OnGiantZombieStart()
    {
        PlaySound(GiantZombieSound.SoundType.roaring);
        StartCoroutine(startos());
        
    }
    private IEnumerator startos()
    {
        animator.SetTrigger("roaring");
        yield return new WaitForSeconds(2);
        PlaySound(GiantZombieSound.SoundType.walk);
        start = true;
    }
    private void Update()
    {
        if (healthSystem.GetIsDead())
        {
            agent.isStopped = true;
            animator.SetLayerWeight(1, 0);
            animator.SetBool("dead", true);
            stageManeger.IncreasDeadBosses();
            audioSource.enabled = false;
            Destroy(audioSource);
            Destroy(agent);
            Destroy(this);


        }
        else if (start)
        {
            if (agent != null) agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            animator.SetBool("walk", true);


            if (isAttacking)
                return;
            // Select a random attack animation
            int randomIndex = Random.Range(0, _attackAnimations.Count);
            EnemyAttackAnimation attackAnimation = _attackAnimations[randomIndex];
            // Check if player is within attack range
            if (attackAnimation.Distance >= Vector3.Distance(transform.position, player.transform.position))
            {
                isAttacking = true;
                // Play the selected attack animation
                PlaySound(GiantZombieSound.SoundType.attack);
                animator.SetTrigger(randomIndex.ToString());
                damage = attackAnimation.damage;
                Invoke(nameof(doDamage), attackAnimation.timeToDoDamage);
                Invoke(nameof(StopAnimation), attackAnimation.animationTime);
                agent.speed = attackSpeed;
            }



        }
    }
    private void StopAnimation()
    {
        // Reset attack variables
        isAttacking = false;
        PlaySound(GiantZombieSound.SoundType.walk);
        agent.speed = runSpeed;

    }
    private void doDamage()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position + transform.forward + attackPos, rd);
        foreach (Collider col in nearbyColliders)
            if (col.gameObject.CompareTag("Player"))
                player.GetComponent<HealthSystem>().TakeDamage(damage);
    }
    private void PlaySound(GiantZombieSound.SoundType soundtype)
    {
        List<GiantZombieSound> clip = new List<GiantZombieSound>();
        foreach(GiantZombieSound sound in sounds)
        {
            if (sound.soundType == soundtype) clip.Add(sound);
        }
        clip.ToArray();
        GiantZombieSound[] a = clip.ToArray();
        int randomNum = Random.Range(0, a.Length);
        GiantZombieSound activeSound = clip[randomNum];
        audioSource.clip = activeSound.audioClip;
        audioSource.loop = activeSound.loop;
        audioSource.volume = activeSound.volume;
        audioSource.Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.forward + attackPos, rd);
    }

}
[System.Serializable]
public class GiantZombieSound
{
    public enum SoundType
    {
        idle,
        walk,
        attack,
        roaring,
        dead
    }
    public SoundType soundType;
    public AudioClip audioClip;
    public bool loop;
    public float volume;

}
