using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public List<EnemyAudios> idleAudioClip;
    public List<EnemyAudios> walkAudioClip;
    public List<EnemyAudios> chasingAudioClip;
    public List<EnemyAudios> hitAudioClip;
    public List<EnemyAudios> attackAudioClip;
    public List<EnemyAudios> screamAudioClip;
    public List<EnemyAudios> dieAudioClip;

    public AudioSource s;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayIdleAudio()
    {
        if (idleAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, idleAudioClip.Count);
            s.clip = idleAudioClip[randomIndex].audioClip;
            s.volume = idleAudioClip[randomIndex].volume;
            s.pitch = idleAudioClip[randomIndex].pitch;
            s.loop = idleAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayWalkAudio()
    {
        if (walkAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, walkAudioClip.Count);
            s.clip = walkAudioClip[randomIndex].audioClip;
            s.volume = walkAudioClip[randomIndex].volume;
            s.pitch = walkAudioClip[randomIndex].pitch;
            s.loop = walkAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayChasingAudio()
    {
        if (chasingAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, chasingAudioClip.Count);
            s.clip = chasingAudioClip[randomIndex].audioClip;
            s.volume = chasingAudioClip[randomIndex].volume;
            s.pitch = chasingAudioClip[randomIndex].pitch;
            s.loop = chasingAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayScreamAudio()
    {
        if (screamAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, screamAudioClip.Count);
            s.clip = screamAudioClip[randomIndex].audioClip;
            s.volume = screamAudioClip[randomIndex].volume;
            s.pitch = screamAudioClip[randomIndex].pitch;
            s.loop = screamAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayHitAudio()
    {
        if (hitAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, hitAudioClip.Count);
            s.clip = hitAudioClip[randomIndex].audioClip;
            s.volume = hitAudioClip[randomIndex].volume;
            s.pitch = hitAudioClip[randomIndex].pitch;
            s.loop = hitAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayAttackAudio()
    {
        if (attackAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, attackAudioClip.Count);
            s.clip = attackAudioClip[randomIndex].audioClip;
            s.volume = attackAudioClip[randomIndex].volume;
            s.pitch = attackAudioClip[randomIndex].pitch;
            s.loop = attackAudioClip[randomIndex].loop;
            s.Play();
        }
    }
    public void PlayDieAudio()
    {
        if (dieAudioClip.Count > 0)
        {
            int randomIndex = Random.Range(0, dieAudioClip.Count);
            s.clip = dieAudioClip[randomIndex].audioClip;
            s.volume = dieAudioClip[randomIndex].volume;
            s.pitch = dieAudioClip[randomIndex].pitch;
            s.loop = dieAudioClip[randomIndex].loop;
            s.Play();
        }
    }
}
[System.Serializable]
public class EnemyAudios
{
    public string audioName;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    public bool loop;
}
