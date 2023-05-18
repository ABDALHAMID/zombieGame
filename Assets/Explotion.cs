using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    public float radius;
    public float explosionForce;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider collider in overlaps)
        {
            HealthSystem hs = collider.GetComponent<HealthSystem>();
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(explosionForce, transform.position, radius);
            Vector3 offset = collider.transform.position - transform.position;
            float sqrlen = offset.sqrMagnitude * radius / 100;
            Debug.Log("obgect: " + collider.gameObject + ", magnitude: " + sqrlen);
            if (hs != null) hs.TakeDamage(damage * sqrlen);

            
        }
    }
}
