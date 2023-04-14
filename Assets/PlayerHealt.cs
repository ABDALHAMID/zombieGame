using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHealt : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Volume _damageVoulume;
    public float damageTime;

    public void DamageTaken()
    {
        _damageVoulume.weight = 1;
        Invoke(nameof(ExitDamageTaken), damageTime);

    }
    private void ExitDamageTaken()
    {
        _damageVoulume.weight = 0;
    }
}
