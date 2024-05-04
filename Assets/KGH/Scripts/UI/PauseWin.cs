using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWin : MonoBehaviour
{
    public GameObject pauseWin;

    public GameObject helpWin;


    public void Continue()
    {
        UiManager.instance.isPauseWin = false;
        Time.timeScale = 1;
        pauseWin.SetActive(false);
    }

    public void CheckPointStart()
    {
        UiManager.instance.isPauseWin = false;
        pauseWin.SetActive(false);
        Time.timeScale = 1;
        DataManager.instance.LoadData();
    }
    public void Restart()
    {   
        UiManager.instance.isPauseWin=false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void HelpWin()
    {
        helpWin.SetActive(true);
    }
    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
}
