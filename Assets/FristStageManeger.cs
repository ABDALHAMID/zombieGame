using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class FristStageManeger : MonoBehaviour
{
    public StarterAssetsInputs _InputSystem;
    public GameObject _player, _getIntoTheWarehouseButon;
    public GameObject guide;
    public Animator _warehouseAnimator;
    public AudioSource audioSource;
    public bool executButton;
    public LayerMask _playerLayerMask;
    public Vector3 _openDoorCheckPoit, _openDoorHalfExtence, _nextLevelCheckPoint, _nextLevelHalfExtence, _guideCheckPoint, _guideHalfExtence;
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
                audioSource.Play();
            }
        }
        else _getIntoTheWarehouseButon.SetActive(false);

        if (Physics.CheckBox(_nextLevelCheckPoint, _nextLevelHalfExtence, Quaternion.identity, _playerLayerMask))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        guide.SetActive(Physics.CheckBox(_guideCheckPoint, _guideHalfExtence + transform.forward, Quaternion.identity, _playerLayerMask));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_openDoorCheckPoit, _openDoorHalfExtence);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_nextLevelCheckPoint, _nextLevelHalfExtence);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_guideCheckPoint, _guideHalfExtence + transform.forward);
    }
}
