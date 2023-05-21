using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
public class SeconStageManeger : MonoBehaviour
{
    public StarterAssetsInputs _InputSystem;

    public LayerMask playerLayer;
    public Vector3 bossesStartCheckBoxPosition;
    public Vector3 bossesStartCheckBoxSize;
    public Vector3 enableCamerasCheckBoxPosition;
    public Vector3 enableCamerasCheckBoxSize;
    public Vector3 mainMissionCheckBoxPosition;
    public Vector3 mainMissionCheckBoxSize;
    public Vector3 stuckDoorCheckBoxPosition;
    public Vector3 stuckDoorCheckBoxSize;
    public OpenDoor stuckDoor;
    public GameObject stuckDoorUI;
    public Animator[] capsulseToOpen;
    public GiantZombieControler[] bosses;
    public Animator bossRoomDoorAnimator;
    public bool showButton;
    public GameObject exticutionButton;
    public Camera[] cameras;
    [HideInInspector]
    public bool executButton;
    private bool alreadyStart;
    public int BossDead;
    public Animator lastDoorToOpen;
    private bool alreadyCamerasEnabled;
    private int objectivCompleated;
    public GameObject accessNeadedPanel;
    public GameObject objectiveUI;
    private bool camerasDisabled;
    private bool dataGetit;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
         executButton = _InputSystem.Execute;
        if(Physics.CheckBox(mainMissionCheckBoxPosition, mainMissionCheckBoxSize, Quaternion.identity, playerLayer) && !dataGetit)
        {
            exticutionButton.SetActive(true);
            if (executButton)
            {

                if (objectivCompleated >= 5)
                {
                    MissionDone();
                    dataGetit = true;
                }
                else 
                {
                    accessNeadedPanel.SetActive(true);
                    
                }
            }
            else
            {
                accessNeadedPanel.SetActive(false);

            }
        }
        else
        {
            exticutionButton.SetActive(false);

        }

        if (Physics.CheckBox(stuckDoorCheckBoxPosition, stuckDoorCheckBoxSize, Quaternion.identity, playerLayer))
        {
            stuckDoor.checkBoxSize = Vector3.zero;
            stuckDoorUI.SetActive(true);
        }
        else stuckDoorUI.SetActive(false) ;
        if (_InputSystem.Objective)
        {
            EnableCameras(5);
            objectiveUI.SetActive(true);
        }
        else objectiveUI.SetActive(false);

        if (Physics.CheckBox(bossesStartCheckBoxPosition, bossesStartCheckBoxSize, Quaternion.identity, playerLayer) && !alreadyStart)
        {
            OpenCapsules();
            Invoke(nameof(StartBattle), 3);
            alreadyStart = false;
        }
        if (Physics.CheckBox(enableCamerasCheckBoxPosition, enableCamerasCheckBoxSize, Quaternion.identity, playerLayer) && !alreadyCamerasEnabled)
        {
            EnableCameras(cameras.Length);
            alreadyCamerasEnabled = true;
        }
        else if (alreadyCamerasEnabled)
        {
            alreadyCamerasEnabled = false;
        }




        camerasDisabled = true;
        if (alreadyCamerasEnabled || _InputSystem.Objective) camerasDisabled = false;
        if (camerasDisabled) DisableCameras();

        if (BossDead >= bosses.Length) lastDoorToOpen.SetBool("openDor", true);
    }
    public void IncreasDeadBosses()
    {
        ++BossDead;
    }
    public void IncreasObjectivCompleated()
    {
        ++objectivCompleated;
    }
    private void EnableCameras(int numberOfCameras)
    {
        for(int i = 0; i < numberOfCameras; i++)
        {
            cameras[i].enabled = true;
        }
    }
    private void MissionDone()
    {
        Debug.Log("Mission Done");
        Invoke(nameof(NextSceane), 5f);
    }
    private void NextSceane()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void DisableCameras()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
    }
    private void OpenCapsules()
    {
        foreach(Animator capsule in capsulseToOpen)
        {
            capsule.SetTrigger("open");
        }
    }
    private void StartBattle()
    {
        bossRoomDoorAnimator.SetBool("openDor", false);
        bossRoomDoorAnimator.SetTrigger("close");
        foreach (GiantZombieControler boss in bosses)
        {
            boss.OnGiantZombieStart();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(bossesStartCheckBoxPosition, bossesStartCheckBoxSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(enableCamerasCheckBoxPosition, enableCamerasCheckBoxSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mainMissionCheckBoxPosition, mainMissionCheckBoxSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(stuckDoorCheckBoxPosition, stuckDoorCheckBoxSize);
    }
}
