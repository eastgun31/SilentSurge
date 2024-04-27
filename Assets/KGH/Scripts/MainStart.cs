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

    public void StartButton()
    {
        mainButton.SetActive(false);
        modChoice.SetActive(true);
        backButton.SetActive(true);
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
}
