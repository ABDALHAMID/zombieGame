using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlePanelScript : MonoBehaviour
{
    public LayerMask playerLayer;
    public Vector3 checkBoxPosition;
    public Vector3 checkBoxSize;
    public Slider slider;
    [Range(0, 1)]
    private float sliderValue;
    public SeconStageManeger seconStage;
    public GameObject showButton;
    public GameObject loadingMSG;
    public AudioSource audio;
    public GameObject ui;
    public AudioClip loodingSound;
    public AudioClip FinishgSound;
    private bool play;
    private bool done;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = sliderValue;
        if (sliderValue >= 1 && !done)
        {
            seconStage.IncreasObjectivCompleated();
            audio.clip = FinishgSound;
            audio.loop = false;
            audio.Play();
            ui.SetActive(false);
            done = true;
        }
        if (Physics.CheckBox(checkBoxPosition + transform.position, checkBoxSize, Quaternion.identity, playerLayer) && !done)
        {
            showButton.SetActive(true);
            if (seconStage.executButton)
            {
                sliderValue += Time.deltaTime * .3f;
                loadingMSG.SetActive(true);
                audio.clip = loodingSound;
                audio.loop = true;
                if(!play)audio.Play();
                play = true;
            }
            else
            {
                loadingMSG.SetActive(false);
                audio.clip = loodingSound;
                audio.Stop();
                play = false;
            }
                
        }
        else showButton.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(checkBoxPosition + transform.position, checkBoxSize);
    }
}
