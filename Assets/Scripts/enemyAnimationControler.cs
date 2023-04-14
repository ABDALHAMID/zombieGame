using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class enemyAnimationControler : MonoBehaviour
{
    private BaseEnemyControle enemyControle;
    private HealthSystem healthSystem;
    private EnemyAttacks enemyAttacks;
    private Animator anim;
    private int enemyState;
    private bool checkForAnimationEnd = false;
    [SerializeField] 
    private string[] attackAnimationName;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyControle = GetComponent<BaseEnemyControle>();
        enemyAttacks = GetComponent<EnemyAttacks>();
        anim = GetComponentInChildren<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        //check if the enemey is dead or attacking or moving and apply the correct animation
        if (healthSystem.GetIsDead()) enemyState = 0;
        else if (enemyAttacks.GetIsAtacking()) enemyState = enemyAttacks.GetCurantState();

        else enemyState = enemyControle.GetCurantState();
        anim.SetInteger("State", enemyState);

        foreach (string attackAnimationName in attackAnimationName)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName) && !checkForAnimationEnd) checkForAnimationEnd = true;
            if (enemyAttacks.GetIsAtacking() && !anim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName) && checkForAnimationEnd)
            {
                checkForAnimationEnd = false;
                enemyAttacks.AttackEnd();
                Debug.Log("animation end");
            }
        }

    }

}
