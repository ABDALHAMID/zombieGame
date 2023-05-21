using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class FristStageManeger : MonoBehaviour
{
    public StarterAssetsInputs _InputSystem;
    public GameObject _player, _getIntoTheWarehouseButon;
    public Animator _warehouseAnimator;
    public AudioSource audio;
    public bool executButton;
    public LayerMask _playerLayerMask;
    public Vector3 _openDoorCheckPoit, _openDoorHalfExtence, _nextLevelCheckPoint, _nextLevelHalfExtence;
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        executButton = _InputSystem.Execute;
        if (Physics.CheckBox(_openDoorCheckPoit, _openDoorHalfExtence, Quaternion.identity, _playerLayerMask))
        {
            _getIntoTheWarehouseButon.SetActive(true);
            if (_InputSystem.Execute)
            {
                _warehouseAnimator.SetBool("openDor", true);
                audio.Play();
            }
        }
        else _getIntoTheWarehouseButon.SetActive(false);

        if (Physics.CheckBox(_nextLevelCheckPoint, _nextLevelHalfExtence, Quaternion.identity, _playerLayerMask))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_openDoorCheckPoit, _openDoorHalfExtence);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_nextLevelCheckPoint, _nextLevelHalfExtence);
    }
}
