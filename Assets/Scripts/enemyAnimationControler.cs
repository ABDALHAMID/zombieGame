using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class enemyAnimationControler : MonoBehaviour
{
    private BaseEnemyControle enemyControle;
    private healthSystem healthSystem;
    private enemyAttacks enemyAttacks;
    private Animator anim;
    private int enemyState;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyControle = GetComponent<BaseEnemyControle>();
        enemyAttacks = GetComponent<enemyAttacks>();
        anim = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<healthSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (healthSystem.GetIsDead()) enemyState = 0;
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Punching")) enemyState = enemyAttacks.getCurantState();
        else enemyState = enemyControle.getCurantState();
        anim.SetInteger("State", enemyState);
    }
}
