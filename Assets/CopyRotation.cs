using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public Transform _targetObjet;
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation.Set(0, 0, _targetObjet.rotation.y, 0);
    }
}
