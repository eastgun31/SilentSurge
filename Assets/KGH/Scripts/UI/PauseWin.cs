using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWin : MonoBehaviour
{
    public GameObject pauseWin;

    public GameObject helpWin;
    public GameObject page1;
    public GameObject page2;


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
    public void GoToPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }
    public void GoToPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }
}
