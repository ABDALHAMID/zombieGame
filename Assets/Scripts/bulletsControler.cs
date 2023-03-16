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


    //get the information about the object hit by bullet
    private void OnCollisionEnter(Collision collision)
    {
        //get what object we hit
        whatWeHit = collision.gameObject;

        //run damage function
        DoDamage();

        check = false;

        
    }

    //damage function
    private void DoDamage()
    {
        //check if we hit something for the first time
        if(check && whatWeHit != null)
        {
            //check if he can take damge
            if (whatWeHit.CompareTag("Enemy") || whatWeHit.CompareTag("Destroyerble"))
            {
                //call the takedamage script of the health system of the hiting object
                whatWeHit.GetComponent<healthSystem>().TakeDamage(damage);
                DestroyBulletAfter();
            }
            //if the hitted object is not capable to get damage
            else
            {
                /*
                 * sctipt not complete
                 * make un visual hit spote 
                 */
                Debug.Log("we hit samething \n script \"bulletsControler\" not complet!!!");
                rb.useGravity = true;
                Invoke(nameof(DestroyBulletAfter), 5);
            }
        }        
    }
    private void DestroyBulletAfter()
    {
        //run the function for destroy the bullet to improve the performance
        //--!!!!!!!!!!!!!!!!!!important--
        //replace this with the pool of object when we have it
        Destroy(this.gameObject);
    }
    private int GetDamage()
    {
        return damage;
    }
    private void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
