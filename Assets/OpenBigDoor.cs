using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBigDoor : MonoBehaviour
{
    public LayerMask playerLayer;
    public Vector3 checkBoxPosition;
    public Vector3 checkBoxSize;
    public SeconStageManeger maneger;
    public Animator doorAniamtor;
    private bool done;
    private AudioSource audioSource;
    public GameObject showButton;
    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(!done)
        if (Physics.CheckBox(checkBoxPosition + transform.position, checkBoxSize, Quaternion.identity, playerLayer))
        {
                showButton.SetActive(true);
                if (maneger.executButton)
                {
                    doorAniamtor.SetBool("openDor", true);
                    audioSource.Play();
                    done = true;
                }

        }
        else showButton.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(checkBoxPosition + transform.position, checkBoxSize);
    }
}
