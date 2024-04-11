using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager Ui_instance;
    public static UiManager instance { get { return Ui_instance; } }

    public GameObject missionTime;
    public Text success;
    public float timeRemainig = 15f;
    public bool isWin = false;

    public GameObject pipePuzFst;
    public GameObject pipePuzSec;

    public GameObject pipeKeypadNumTwo;
    public GameObject pipeKeypadNumThree;
    public GameObject pipeKeypadNumFour;
    public GameObject pipeKeypadNumFive;
    public GameObject pipeKeypadNumSix;
    public GameObject pipeKeypadNumSeven;
    public GameObject pipeKeypadNumEight;
    public GameObject pipeKeypadNumNine;
    public GameObject pipeKeypadNumZero;

    public GameObject keypadFst;

    public GameObject sinPuzzleFst;


    public void Awake()
    {
        if (Ui_instance != null)
            Destroy(gameObject);
        else
            Ui_instance = this;
    }

    public void TimeRemainig() // 퍼즐 제한시간
    {
        if (!isWin)
        {
            if ((int)timeRemainig == 0)
            {
                Debug.Log("fail");
                success.text = "FAIL";
            }
            else
            {
                timeRemainig -= Time.deltaTime;
                success.text = "Time: " + (int)timeRemainig;
            }
        }
        else
        {
            success.text = "SUCCESS";
            Invoke("ResetTime", 2f);
        }
    }
    private void ResetTime()
    {
        timeRemainig = 15f;
        isWin = false; 
    }
    public void ActivePipeFst()
    {
        pipePuzFst.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeSec()
    {
        pipePuzSec.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumTwo()
    {
        pipeKeypadNumTwo.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumThree()
    {
        pipeKeypadNumThree.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumFour()
    {
        pipeKeypadNumFour.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumFive()
    {
        pipeKeypadNumFive.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumSix()
    {
        pipeKeypadNumSix.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumSeven()
    {
        pipeKeypadNumSeven.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumEight()
    {
        pipeKeypadNumEight.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumNine()
    {
        pipeKeypadNumNine.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumZero()
    {
        pipeKeypadNumZero.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActiveKeypad()
    {
        keypadFst.SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActiveSinFst()
    {
        sinPuzzleFst.SetActive(true);
        missionTime.SetActive(true);
    }
    public void CloseSinFst()
    {
        sinPuzzleFst.SetActive(false);
        missionTime.SetActive(false);
    }
    public void CloseKeypadFst() 
    {
        keypadFst.SetActive(false);
        missionTime.SetActive(false);
    }
}
