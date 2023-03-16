using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class bodyAimControler : MonoBehaviour
{
    public Transform aimPoint;
    public float angel = 0, minWeight = .2f, timeAnimation;
    private Rig bodyRigLayer;
    // Start is called before the first frame update
    void Start()
    {
        bodyRigLayer = GetComponent<Rig>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 directionToTarget = aimPoint.position - transform.position;
        float dotProduct = Vector3.Dot(directionToTarget.normalized, transform.forward);

        if (dotProduct < angel)
        {
            bodyRigLayer.weight = Mathf.Lerp(bodyRigLayer.weight, minWeight, timeAnimation);
        }
        else
        {
            bodyRigLayer.weight = Mathf.Lerp(bodyRigLayer.weight, 1, timeAnimation);
        }
    }
}
