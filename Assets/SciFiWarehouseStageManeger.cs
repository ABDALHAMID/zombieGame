using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SciFiWarehouseStageManeger : MonoBehaviour
{
    public UnityEvent _StageEvents;
    public Vector3 _playerCheckPositionZombieStart, _playerCheckBoxZombieStart;
    public LayerMask _plyerLayer;

    private void Update()
    {
        if (Physics.CheckBox(_playerCheckPositionZombieStart, _playerCheckBoxZombieStart, Quaternion.identity, _plyerLayer))
        {
            _StageEvents.Invoke();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_playerCheckPositionZombieStart, _playerCheckBoxZombieStart);
    }
}
