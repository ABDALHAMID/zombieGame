using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletsControler : MonoBehaviour
{
    private GameObject whatWeHit;
    public int damage;
    private void OnCollisionEnter(Collision collision)
    {
        whatWeHit = collision.gameObject;
        DoDamage();
        
    }
    private void DoDamage()
    {
        if(whatWeHit != null)
        {
            if(whatWeHit.tag == "Enemy" || whatWeHit.tag == "Destroyerble")
            {
                whatWeHit.GetComponent<healthSystem>().TakeDamage(damage);
            }
            else
            {
                Debug.Log("we hit a wall");
            }
        }        
    }

}
