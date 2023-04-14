using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTime : MonoBehaviour
{
    public Vector3 _vectorMove;
    void FixedUpdate()
    {
        transform.Translate(_vectorMove);
    }
}
