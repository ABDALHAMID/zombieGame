using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitObjectScript : MonoBehaviour
{
    private GameObject hitedObject;
    public int damage;

    private void OnCollisionEnter(Collision collision)
    {
        hitedObject = collision.gameObject;
    }
    public GameObject GetHitedObject()
    {
        return hitedObject;
    }
}
