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
    public void ClosePipeFst()
    {
        pipePuzFst.SetActive(false);
        missionTime.SetActive(false);
    }
    public void ClosePipeSec()
    {
        pipePuzSec.SetActive(false);
        missionTime.SetActive(false);
    }
    public void ActivePipeSec()
    {
        pipePuzSec.SetActive(true);
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
