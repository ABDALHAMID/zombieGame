using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationControler : MonoBehaviour
{
    public List<EnemyAnimation> idleAnimations;
    public List<EnemyAnimation> chaseAnimations;
    public List<EnemyAnimation> patrolAnimations;
    public List<EnemyAnimation> screamAnimations;
    public List<EnemyAnimation> hitAnimations;
    public string idleBlendTreeName = "idle";
    public string walkBlendTreeName = "walk";
    public string screamBlendTreeName = "scream";
    public string chaseBlendTreeName = "chase";
    public string hitBlendTreeName = "hit";
    public EnemySounds enemySounds;
    public UnityEvent OnStartScreaming;
    public UnityEvent OnStartShasing;
    private int currentHitIndex = 0;

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        enemySounds = GetComponent<EnemySounds>();
    }
    private void Update()
    {
        
    }
    public void PlayIdleAnimation()
    {
        if (idleAnimations.Count > 0)
        {
            _animator.SetInteger("State", 1);
            int randomIndex = Random.Range(0, idleAnimations.Count);
            _animator.SetFloat(idleBlendTreeName, randomIndex);
            enemySounds.PlayIdleAudio();
        }
    }

    public void PlayChaseAnimation()
    {
        if (chaseAnimations.Count > 0)
        {
        _animator.SetInteger("State", 4);
            int randomIndex = Random.Range(0, chaseAnimations.Count);
            _animator.SetFloat(chaseBlendTreeName, randomIndex);
            enemySounds.PlayChasingAudio();
        }
    }

    public void PlayPatrolAnimation()
    {
        if (patrolAnimations.Count > 0)
        {
        _animator.SetInteger("State", 2);
            int randomIndex = Random.Range(0, patrolAnimations.Count);
            _animator.SetFloat(walkBlendTreeName, randomIndex);
            enemySounds.PlayWalkAudio();
        }
    }

    
    public void PlayHitAnimation()
    {
        if (hitAnimations.Count > 0)
        {
            _animator.SetInteger("State", 6);
            if (currentHitIndex >= hitAnimations.Count) currentHitIndex = 0;
            Debug.Log(currentHitIndex);
            _animator.SetFloat(hitBlendTreeName, currentHitIndex);
            currentHitIndex++;
            enemySounds.PlayHitAudio();
        }
    }

    public void PlayScreamAnimation()
    {
        if (screamAnimations.Count > 0)
        {
            _animator.SetInteger("State", 3);
            int randomIndex = Random.Range(0, screamAnimations.Count);
            _animator.SetFloat(screamBlendTreeName, randomIndex);
            StartCoroutine(ScreamToChase(randomIndex));
            enemySounds.PlayScreamAudio();

        }
    }
    
    IEnumerator ScreamToChase(int currentIndex)
    {
        OnStartScreaming.Invoke();
        yield return new WaitForSeconds(screamAnimations[currentIndex].animationTime);
        OnStartShasing.Invoke();
    }
    
    public void OnDie()
    {
        _animator.SetInteger("State", 0);
        enemySounds.PlayDieAudio();
        Destroy(enemySounds);
        Destroy(this);
    }
}
[System.Serializable]
public class EnemyAnimation
{
    public string AnimationName;
    public float animationTime;
}