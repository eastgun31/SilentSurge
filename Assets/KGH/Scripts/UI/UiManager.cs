using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyLevel;

public class UiManager : MonoBehaviour
{
    private static UiManager Ui_instance;
    public static UiManager instance { get { return Ui_instance; } }

    public GameObject missionTime;
    public Text success;
    private float timeRemainig;
    public bool isWin = false;

    public GameObject pipePuzFst;
    public GameObject pipePuzSec;

    public GameObject[] pipeKeypadNum;

    public GameObject keypadFst;

    public GameObject sinPuzzleFst;

    public GameObject gameover;


    private void Start()
    {
        if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            timeRemainig = 15f;
        else if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            timeRemainig = 10f;
        else if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            timeRemainig = 7f;

    }
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
                gameover.SetActive(true);
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
        pipeKeypadNum[0].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumThree()
    {
        pipeKeypadNum[1].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumFour()
    {
        pipeKeypadNum[2].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumFive()
    {
        pipeKeypadNum[3].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumSix()
    {
        pipeKeypadNum[4].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumSeven()
    {
        pipeKeypadNum[5].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumEight()
    {
        pipeKeypadNum[6].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumNine()
    {
        pipeKeypadNum[7].SetActive(true);
        missionTime.SetActive(true);
    }
    public void ActivePipeKeypadNumZero()
    {
        pipeKeypadNum[8].SetActive(true);
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
