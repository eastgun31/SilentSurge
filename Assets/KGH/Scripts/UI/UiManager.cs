using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyLevel;

public class UiManager : MonoBehaviour
{
    private static UiManager Ui_instance;
    public static UiManager instance { get { return Ui_instance; } }

    public float timeRemainig;
    public bool isWin = false;
    public bool isPauseWin = false;
    public bool isSubWin = false;
    public bool isGameOver = false;

    public GameObject quickSlot;

    public GameObject pauseWin;

    public GameObject pipePuzFst;
    public GameObject pipePuzSec;

    public GameObject[] pipeKeypadNum;

    public GameObject keypadFst;

    public GameObject sinPuzzleFst;

    public GameObject hackingpuzFst; 

    public GameObject gameover;


    private void Update()
    {
        ActivePauseWin();
    }
    public void Awake() 
    {
        if (Ui_instance != null)
            Destroy(gameObject);
        else
            Ui_instance = this;
    }

    public void TimeLimit()
    {
        if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            timeRemainig = 15f;
        else if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            timeRemainig = 10f;
        else if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            timeRemainig = 7f;
    }

    private void ResetTime()
    {
        isWin = false; 
    }
    private void ActivePauseWin()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseWin&&!isGameOver)
            {
                Time.timeScale = 0;
                pauseWin.gameObject.SetActive(true);
                isPauseWin = true;
            }
            else
            {
                if (isSubWin == false)
                {
                    Debug.Log("1");
                    Time.timeScale = 1;
                    pauseWin.gameObject.SetActive(false);
                    isPauseWin = false;
                }
            }
        }
    }

    public void ActivePipeFst()
    {
        TimeLimit();
        pipePuzFst.SetActive(true);
    }
    public void ActivePipeSec()
    {
        TimeLimit();
        pipePuzSec.SetActive(true);
    }
    public void ActivePipeKeypadNumTwo()
    {
        TimeLimit();
        pipeKeypadNum[0].SetActive(true);
    }
    public void ActivePipeKeypadNumThree()
    {
        TimeLimit();
        pipeKeypadNum[1].SetActive(true);
    }
    public void ActivePipeKeypadNumFour()
    {
        TimeLimit();
        pipeKeypadNum[2].SetActive(true);
    }
    public void ActivePipeKeypadNumFive()
    {
        TimeLimit();
        pipeKeypadNum[3].SetActive(true);
    }
    public void ActivePipeKeypadNumSix()
    {
        TimeLimit();
        pipeKeypadNum[4].SetActive(true);
    }
    public void ActivePipeKeypadNumEight()
    {
        TimeLimit();
        pipeKeypadNum[6].SetActive(true);
    }
    public void ActivePipeKeypadNumNine()
    {
        TimeLimit();
        pipeKeypadNum[7].SetActive(true);
    }
    public void ActivePipeKeypadNumZero()
    {
        TimeLimit();
        pipeKeypadNum[8].SetActive(true);
    }
    public void ActiveKeypad()
    {
        TimeLimit();
        keypadFst.SetActive(true);
    }
    public void ActiveSinFst()
    {
        TimeLimit();
        sinPuzzleFst.SetActive(true);
        quickSlot.SetActive(false);
    }
    public void ActiveHackingFst()
    {
        TimeLimit();
        hackingpuzFst.SetActive(true);
    }

    public void CloseSinFst()
    {  
        sinPuzzleFst.SetActive(false);
        quickSlot.SetActive(true);
    }
    public void CloseKeypadFst() 
    {
        keypadFst.SetActive(false);
    }
}
