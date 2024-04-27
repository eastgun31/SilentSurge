using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWin : MonoBehaviour
{
    public GameObject pauseWin;
    public GameObject optionWin;
    public GameObject helpWin;


    public void Continue()
    {
        Time.timeScale = 1;
        pauseWin.SetActive(false);
    }

    public void CheckPointStart()
    {
        // 체크포인트 연결
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OptionWin()
    {
        optionWin.SetActive(true);
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
