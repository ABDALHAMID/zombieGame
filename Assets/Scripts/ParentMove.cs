using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMove : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.parent.parent.position = transform.position - transform.parent.position;
        transform.parent.parent.rotation = transform.rotation;
    }
}
