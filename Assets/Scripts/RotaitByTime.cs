using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaitByTime : MonoBehaviour
{
    public float _rotationSpeed;
    public bool ZRotation;
    void Update()
    {
        if(ZRotation) transform.Rotate(new Vector3(0, 0, _rotationSpeed));
        else transform.Rotate(new Vector3(_rotationSpeed, 0, 0));
    }
}
