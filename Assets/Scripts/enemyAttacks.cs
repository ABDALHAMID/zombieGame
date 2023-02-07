using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyAttacks : MonoBehaviour
{
    private BaseEnemyControle enemyControle;
    private int curantState;
    private Animator animator;
    private healthSystem healthSystem;


    // Start is called before the first frame update
    void Start()
    {
        enemyControle = GetComponent<BaseEnemyControle>();
        healthSystem = GetComponent<healthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.GetIsDead()) Destroy(this);
    }
    public void Attack()
    {
        curantState = 4;
        enemyControle.setIsAttacking(false);
        Debug.Log("we are attacking");
    }
    public int getCurantState()
    {
        return curantState;
    }
}
