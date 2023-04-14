using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttacks : MonoBehaviour
{
    private bool isAttaking;
    private int curantState;
    private HealthSystem healthSystem;
    public EnemyHitObjectScript[] hitObjects;
    private BaseEnemyControle enemyControle;
    private bool doDamage = false;


    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        enemyControle = GetComponent<BaseEnemyControle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.GetIsDead()) Destroy(this);
        if (doDamage) DoDamge();
    }
    public void Attack()
    {
        Debug.Log("Attck Function");
        isAttaking = true;
        curantState = 4;
        doDamage = true;
        enemyControle.SetAttackOneTime(false);
        enemyControle.SetIsAttacking(true);
    }
    private void DoDamge()
    {
        
        foreach (EnemyHitObjectScript hitObject in hitObjects)
        {
            if (hitObject.GetHitedObject() != null && hitObject.GetHitedObject().CompareTag("Player"))
            {
                HealthSystem hitedObjectHealtSystem = hitObject.GetHitedObject().GetComponentInParent<HealthSystem>();
                hitedObjectHealtSystem.TakeDamage(hitObject.damage);
                Debug.Log("do Damage");
                doDamage = false;
            }
        }
    }
    public void AttackEnd()
    {
        Debug.Log("attackend");
        isAttaking = false;
        enemyControle.SetIsAttacking(false);
        enemyControle.SetAttackOneTime(true);
    }
    public int GetCurantState() => curantState;
    public bool GetIsAtacking() =>  isAttaking;
}
