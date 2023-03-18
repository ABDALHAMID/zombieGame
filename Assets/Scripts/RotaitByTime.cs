using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaitByTime : MonoBehaviour
{
    public float _rotationSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(_rotationSpeed, 0, 0));
    }
}
