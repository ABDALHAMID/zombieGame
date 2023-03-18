using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneScript : MonoBehaviour
{
    [SerializeField]
    private int damage;
    //check if we can collide agame
    //get the information about the object hit by bullet
    private void OnCollisionEnter(Collision collision)
    {
        //get what object we hit
        GameObject whatWeHit = collision.gameObject;
        Debug.Log(whatWeHit);
        //run damage function
        if (whatWeHit != null && whatWeHit.GetComponent<healthSystem>() != null)
            DoDamage(whatWeHit);



    }

    //damage function
    private void DoDamage(GameObject whatWeHit)
    {
        //check if we hit something for the first time
        
            whatWeHit.GetComponent<healthSystem>().TakeDamage(damage);
    }
}
