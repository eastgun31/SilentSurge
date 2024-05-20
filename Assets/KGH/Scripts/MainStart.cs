using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
    public GameObject mainButton;
    public GameObject modChoice;
    public GameObject Sound;
    public GameObject infoWin;
    public GameObject backButton;
    public GameObject help;
    public GameObject page1;
    public GameObject page2;

    public GameObject c_Page1;
    public GameObject c_Page2;

    public GameObject stage2Ban;
    public GameObject stage3Ban;

    public void StartButton()
    {
        mainButton.SetActive(false);
        modChoice.SetActive(true);
        backButton.SetActive(true);

        if(SoundManager.instance.stage1Clear)
            stage2Ban.SetActive(false);

        if(SoundManager.instance.stage2Clear)
            stage3Ban.SetActive(false);
    }

    public void BackButton()
    {
        mainButton.SetActive(true);
        modChoice.SetActive(false);
        backButton.SetActive(false);
    }

    public void NormalStart()
    {
        
    }

    public void InfoWin()
    {
        infoWin.SetActive(true);
    }
    public void CloseInfo()
    {
        infoWin.SetActive(false);
    }
    public void CloseSound()
    {
        Sound.SetActive(false);
    }

    public void HelpWin()
    {
        help.SetActive(true);
    }
    public void CloseHelp()
    {
        help.SetActive(false);
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
    public void GoToC_Page1()
    {
        c_Page1.SetActive(true);
        c_Page2.SetActive(false);
    }
    public void GoToC_Page2()
    {
        c_Page1.SetActive(false);
        c_Page2.SetActive(true);
    }
}
