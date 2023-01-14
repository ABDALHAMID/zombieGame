using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class enemyAnimationControler : MonoBehaviour
{
    private BaseEnemyControle enemyControle;
    private Animator anim;
    private int enemyState;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyControle = GetComponent<BaseEnemyControle>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        enemyState = enemyControle.getCurantState();
        anim.SetInteger("State", enemyState);
        Debug.Log(enemyState);
    }
}
