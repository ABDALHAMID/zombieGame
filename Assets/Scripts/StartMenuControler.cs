using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControler : MonoBehaviour
{
    public GameObject _mainScreenPanel, _levelSelect, _startGameOptions, _optionPanel;
    // Start is called before the first frame update
    void Start()
    {
        _mainScreenPanel.SetActive(true);
        _startGameOptions.SetActive(false);
        _levelSelect.SetActive(false);
        _optionPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        _mainScreenPanel.SetActive(false);
        _startGameOptions.SetActive(true);
    }

    public void Option()
    {
        _mainScreenPanel.SetActive(false);
        _optionPanel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartGameOptionsBack()
    {
        _mainScreenPanel.SetActive(true);
        _startGameOptions.SetActive(false);
    }
    public void LevelSelectBack()
    {
        _startGameOptions.SetActive(true);
        _levelSelect.SetActive(false);
    }
    public void OptionsPanelBack()
    {
        _mainScreenPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }
    public void Contune()
    {
        _startGameOptions.SetActive(false);
        _levelSelect.SetActive(true);
    }


}