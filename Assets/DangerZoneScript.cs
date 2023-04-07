using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneScript : MonoBehaviour
{
    [SerializeField] private int _damagePerSecond;
    [SerializeField] private GameObject _player;
    [SerializeField] private LayerMask _targetObject;
    [SerializeField] private float _radius;
    [SerializeField] private float _timeBetweenDamag, _damageInterval;
    private float timeElapsed = 0f;
    private bool canTakeDamage = true;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        /*healthSystem healthSystem = other.GetComponent<healthSystem>();

        if(healthSystem != null)
        {
            timeElapsed += Time.deltaTime;

            if
             (timeElapsed >= damageInterval)
            {
                healthSystem.TakeDamage(damagePerSecond * damageInterval);
                timeElapsed = 0f;

            }
        }*/
    }

private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach
         (Collider collider in colliders)
        {
            healthSystem healthSystem = collider.GetComponent<healthSystem>();
            Debug.Log(collider.gameObject);
            if(healthSystem != null)
            {
                healthSystem = collider.GetComponentInParent<healthSystem>();
                if (healthSystem != null)
                {
                    timeElapsed += Time.deltaTime;

                    if
                     (timeElapsed >= _damageInterval)
                    {
                        healthSystem.TakeDamage(_damagePerSecond * ((int)_damageInterval));
                        timeElapsed = 0f;
                    }
                }
            }
        }
/*        if (Physics.CheckSphere(transform.position, _raduis, _targetObject) && canTakeDamage)
        {
            canTakeDamage = false;
            Invoke(nameof(DoDamage), _timeBetweenDamag);
        }*/
    }
    //damage function
    private void DoDamage()
    {
        //check if we hit something for the first time
        Debug.Log("dangerzomne" + this.gameObject);
            _player.GetComponent<healthSystem>().TakeDamage(_damagePerSecond);
        canTakeDamage = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.HSVToRGB(0, 50, 100, true);
        Gizmos.DrawSphere(transform.position, _radius);
    }

}
