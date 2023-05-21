using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private List<EnemyAttackAnimation> _attackAnimations;
    private GameObject player;
    public LayerMask playerLayer;
    private BaseEnemyControler baseEnemyControle;
    private Animator _animator;
    public EnemySounds enemySounds;
    private bool _isAttacking, readyToAttak;
    private float damage;
    public string attackBlendTree = "attacking";
    [SerializeField]
    private Vector3 attackPos;
    [SerializeField] private  float rd;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _isAttacking = false;
        readyToAttak = true;
        baseEnemyControle = GetComponent<BaseEnemyControler>();
        enemySounds = GetComponent<EnemySounds>();
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {

        // If already attacking, return
        if (_isAttacking && !readyToAttak)
            return;
        // Select a random attack animation
        int randomIndex = Random.Range(0, _attackAnimations.Count);
        EnemyAttackAnimation attackAnimation = _attackAnimations[randomIndex];
        // Check if player is within attack range
        if (attackAnimation.Distance >= Vector3.Distance(transform.position, player.transform.position))
        {
            readyToAttak = false;
            baseEnemyControle.OnAttackAnimationStart();
            transform.LookAt(player.transform.position);
            // Play the selected attack animation
            _animator.SetInteger("State", 5);
            _animator.SetFloat(attackBlendTree, randomIndex);
            enemySounds.PlayAttackAudio();
            _isAttacking = true;
            damage = attackAnimation.damage;
            Invoke(nameof(doDamage), attackAnimation.timeToDoDamage);
            Invoke(nameof(StopAnimation), attackAnimation.animationTime);
        }
    }

    private void doDamage()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position + transform.forward + attackPos, rd);
        foreach (Collider col in nearbyColliders)
            if (col.gameObject.CompareTag("Player"))
                player.GetComponent<HealthSystem>().TakeDamage(damage);
    }
    private void StopAnimation()
    {
        // Reset attack variables
        _isAttacking = false;
        readyToAttak = true;

        // Call function to handle attack end
        baseEnemyControle.OnAttackAnimationEnd();
    }
    public void OnDie()
    {
        Destroy(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.forward + attackPos, rd);
    }

}

[System.Serializable]
public class EnemyAttackAnimation
{
    public string AnimationName;
    public float Distance;
    public float timeToDoDamage;
    public float animationTime;
    public float damage;
}