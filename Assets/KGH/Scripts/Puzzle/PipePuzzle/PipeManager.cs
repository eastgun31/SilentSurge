using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    public GameObject canvas;

    public GameObject pipesHolder; //파이프를 포함한 부모 오브젝트
    public GameObject[] pipes;

    public GameObject[] items;

    [SerializeField]
    int totalPipes = 0; // 전체 파이프 수
    [SerializeField]
    int correctPipes = 0; // 올바르게 배치된 파이프 수

    private Quaternion[] pipesRot;

    public Text success;

    void Start()
    {
        //전체 파이프수를 설정하고 Pipes배열에 저장

        totalPipes = pipesHolder.transform.childCount;
        pipes = new GameObject[totalPipes];
        pipesRot = new Quaternion[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < pipes.Length; i++)
        {
            pipesRot[i] = pipes[i].transform.rotation;
        }
    }
    private void Update()
    {
       TimeRemainig();
    }

    public void TimeRemainig() // 퍼즐 제한시간
    {
        if (!UiManager.instance.isWin)
        {
            if ((int)UiManager.instance.timeRemainig == 0)
            {
                UiManager.instance.isGameOver = true;
                UiManager.instance.gameover.SetActive(true);
                ClosePipe();
            }
            else
            {
                UiManager.instance.timeRemainig -= Time.deltaTime;
                success.text = "Time:  " + (int)UiManager.instance.timeRemainig;
            }
        }
        else
        {
            success.text = "SUCCESS";
        }
    }
    public void CorrectMove()
    {
        correctPipes += 1;

        if (correctPipes == totalPipes) //승리 조건
        {
            UiManager.instance.isWin = true;

            if (GameManager.instance.puzzleLevel == 1)
            {
                items[0].SetActive(true);
                items[1].SetActive(true);
                GameManager.instance.ItemActive();
                GuideLineTxt.instance.SetDifferentTxt(3);
            }

            if(GameManager.instance.puzzleLevel == 7)
            {
                items[2].SetActive(true);
                items[3].SetActive(true);
                GameManager.instance.ItemActive();
                SubtitleCheck();
            }

            GameManager.instance.puzzleLevel += 1;
            
            Invoke("ClosePipe", 1f);
            DataManager.instance.SaveData();
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        UiManager.instance.isWin = false;
        GameManager.instance.nowpuzzle = false;
        canvas.gameObject.SetActive(false);
        
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].transform.rotation = pipesRot[i];
        }
        correctPipes = 0;
    }
    public void SubtitleCheck()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("씬1");
                GuideLineTxt.instance.SetDifferentTxt(11);
                break;
            case 2:
                Debug.Log("씬1");
                GuideLineTxt.instance.SetDifferentTxt(12);
                break;
        }
    }
}
