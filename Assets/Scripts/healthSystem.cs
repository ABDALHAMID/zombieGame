using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour
{
    public int maxHealt, curruntHealth;

    private void Awake()
    {
        curruntHealth = maxHealt;
    }
    public void TakeDamage(int damageTeken)
    {
        curruntHealth -= damageTeken;
        Die();
    }
    private void Die()
    {
        if(curruntHealth <= 0)
        {
            //die code here
            Destroy(gameObject);
        }
    }
}
