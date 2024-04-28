using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWin : MonoBehaviour
{
    public GameObject pauseWin;

    public GameObject helpWin;
    public GameObject soundWin;


    public void Continue()
    {
        Time.timeScale = 1;
        pauseWin.SetActive(false);
    }

    public void CheckPointStart()
    {
        pauseWin.SetActive(false);
        DataManager.instance.LoadData();
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void HelpWin()
    {
        Debug.Log("2");
        helpWin.SetActive(true);
        UiManager.instance.isSubWin = true;
    }

    public void SoundWin()
    {
        soundWin.SetActive(true);
        UiManager.instance.isSubWin = true ;
    }
    public void CloseSoundWin()
    {
        soundWin.SetActive(false);
        UiManager.instance.isSubWin = false;
    }
    public void CloseHelpWin()
    {
        helpWin.SetActive(false);
        UiManager.instance.isSubWin = false;
    }
    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
}
