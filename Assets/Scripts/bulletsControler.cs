using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletsControler : MonoBehaviour
{
    [SerializeField]
    private float damage;
    //check if we can collide agame
    private bool check = true;

    public bool explosive;
    public GameObject explotion;

    public LayerMask afectedLayer;

    private Rigidbody rb;
    private Explotion explosion;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        explosion = GetComponent<Explotion>();
    }
    //get the information about the object hit by bullet
    private void OnCollisionEnter(Collision collision)
    {

        if (check)
        {
            //get what object we hit
            GameObject whatWeHit = collision.gameObject;
            if(whatWeHit.GetComponent<PrefabVariabels>()) MakeBulletHole(whatWeHit, collision);
            //run damage function
            DoDamage(whatWeHit);

            check = false;
        }  
    }

    //damage function
    private void DoDamage(GameObject whatWeHit)
    {
        
        //check if we hit something for the first time
        if(whatWeHit != null)
        {
            //check if he can take damge
            if (whatWeHit.CompareTag("Enemy") || whatWeHit.CompareTag("Destroyerble"))
            {
                //call the takedamage script of the health system of the hiting object
                if(whatWeHit.GetComponent<HealthSystem>() != null)
                    whatWeHit.GetComponent<HealthSystem>().TakeDamage(damage);
                else
                    whatWeHit.GetComponentInParent<HealthSystem>().TakeDamage(damage);
                DestroyBulletAfter();
            }
            //if the hitted object is not capable to get damage
            else
            {
                /*
                 * sctipt not complete
                 * make un visual hit spote 
                 */

                rb.useGravity = true;
                Invoke(nameof(DestroyBulletAfter), 1f);
            }
            if (explosive)
            {
                Instantiate(explotion, transform.position, Quaternion.identity);
                explosion.Explode();
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

    //put the bullet hole in the collision point
    private void MakeBulletHole(GameObject whatWeHit, Collision collision)
    {
        GameObject bulletHolePrefab = whatWeHit.GetComponent<PrefabVariabels>().bulletHole;
        ContactPoint contact = collision.GetContact(0);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Instantiate(bulletHolePrefab, contact.point + new Vector3(0, 0, 0.15f), rotation);
    
    }
    private float GetDamage()
    {
        return damage;
    }
    private void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
