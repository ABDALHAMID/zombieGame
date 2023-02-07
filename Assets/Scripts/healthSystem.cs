using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    [SerializeField]
    private int maxHealt, curruntHealth;
    private bool isDead = false;

    private void Awake()
    {
        curruntHealth = maxHealt;
    }
    public void TakeDamage(int damageTeken)
    {
        curruntHealth -= damageTeken;
        Die();
    }
    //die function if the curruntHealth is 0 or less
    private void Die()
    {
        if(curruntHealth <= 0)
        {
            //die code here
            isDead = true;
            Debug.Log("we die\n script \"bulletsControler\" not complet!!!");
        }
    }
    public bool GetIsDead()
    {
        return isDead;
    }
}
