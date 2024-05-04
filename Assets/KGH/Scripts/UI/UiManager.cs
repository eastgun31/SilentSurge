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
                Time.timeScale = 1;
                pauseWin.gameObject.SetActive(false);
                isPauseWin=false;
            }
        }
    }

    public int ActivePipeFst()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipePuzFst.SetActive(true);

        return 0;
    }
    public int ActivePipeSec()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipePuzSec.SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumTwo()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[0].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumThree()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[1].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumFour()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[2].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumFive()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[3].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumSix()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[4].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumEight()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[6].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumNine()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[7].SetActive(true);
        return 0;
    }
    public int ActivePipeKeypadNumZero()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        pipeKeypadNum[8].SetActive(true);
        return 0;
    }
    public int ActiveKeypad()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        keypadFst.SetActive(true);
        return 0;
    }
    public int ActiveSinFst()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        sinPuzzleFst.SetActive(true);
        quickSlot.SetActive(false);
        return 0;
    }
    public int ActiveHackingFst()
    {
        GameManager.instance.nowpuzzle = true;
        TimeLimit();
        hackingpuzFst.SetActive(true);
        return 0;
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
