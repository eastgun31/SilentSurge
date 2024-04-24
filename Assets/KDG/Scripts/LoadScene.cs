using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadGameStart()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadStage1_normal()
    {
        SceneManager.LoadScene(1);
    }    
    public void LoadStage1_hard()
    {
        SceneManager.LoadScene(2);
    }    
    public void LoadStage2_normal()
    {

    }    
    public void LoadStage2_hard()
    {

    }
    public void LoadEnding()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
