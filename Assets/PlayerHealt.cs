using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerHealt : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Volume _damageVoulume;
    [SerializeField] private GameObject deadPanel;
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

    public void OnDie()
    {
        this.gameObject.SetActive(false);
        _damageVoulume.weight = 1;
        deadPanel.SetActive(true);
        Invoke(nameof(restart), 2f);
    }

    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
