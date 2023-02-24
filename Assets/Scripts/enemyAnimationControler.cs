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
    private bool checkForAnimationEnd = false;
    [SerializeField] 
    private string[] attackAnimationName;
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
        //check if the enemey is dead or attacking or moving and apply the correct animation
        if (healthSystem.GetIsDead()) enemyState = 0;
        else if (enemyAttacks.GetIsAtacking()) enemyState = enemyAttacks.GetCurantState();

        else enemyState = enemyControle.getCurantState();
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
