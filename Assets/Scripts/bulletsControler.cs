using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletsControler : MonoBehaviour
{
    
    private GameObject whatWeHit;
    [SerializeField]
    private int damage;
    //check if we can collide agame
    private bool check = true;
    private Rigidbody rb;
    private void Awake()
    {
        //get the rigidbody of the bullet
        rb = GetComponent<Rigidbody>();
    }
    //get the information about the object hit by bullet
    private void OnCollisionEnter(Collision collision)
    {
        //get what object we hit
        whatWeHit = collision.gameObject;

        //run damage function
        DoDamage();

        //add the gravity to the bullet so it's realistec
        rb.useGravity = true;

        check = false;

        //run the function for destroy the bullet to improve the performance
        //--!!!!!!!!!!!!!!!!!!important--
        //replace this with the pool of object when we have it
        Invoke("DestroyBulletAfter", 10);
        
    }

    //damage function
    private void DoDamage()
    {
        //check if we hit something for the first time
        if(check && whatWeHit != null)
        {
            //check if he can take damge
            if(whatWeHit.tag == "Enemy" || whatWeHit.tag == "Destroyerble")
            {
                //call the takedamage script of the health system of the hiting object
                whatWeHit.GetComponent<healthSystem>().TakeDamage(damage);
            }
            //if the hitted object is not capable to get damage
            else
            {
                /*
                 * sctipt not complete
                 * make un visual hit spote 
                 */
                Debug.Log("we hit samething \n script \"bulletsControler\" not complet!!!");
            }
        }        
    }
    private void DestroyBulletAfter()
    {
        Destroy(this.gameObject);
    }
    private int getDamage()
    {
        return damage;
    }
    private void setDamage(int newDamage)
    {
        damage = newDamage;
    }
}
