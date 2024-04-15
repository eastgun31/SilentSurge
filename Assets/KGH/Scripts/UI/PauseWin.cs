using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWin : MonoBehaviour
{
    public GameObject pauseWin;
    public GameObject optionWin;
    public GameObject helpWin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        // 게임시작씬 연결
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
        //게임시작화면 씬 연결
    }
}
