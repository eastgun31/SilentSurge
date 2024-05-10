using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    public GameObject canvas;

    public GameObject mapPW;

    public GameObject pipesHolder; //�������� ������ �θ� ������Ʈ
    public GameObject[] pipes;

    public GameObject[] items;

    [SerializeField]
    int totalPipes = 0; // ��ü ������ ��
    [SerializeField]
    int correctPipes = 0; // �ùٸ��� ��ġ�� ������ ��

    private Quaternion[] pipesRot;

    public Text success;

    public UnityEvent doorOpen;

    GameManager gm;
    UiManager um;
    EnemyLevel enemyLevel;

    void Start()
    {
        gm = GameManager.instance;
        um = UiManager.instance;

        //��ü ���������� �����ϰ� Pipes�迭�� ����

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

    public void TimeRemainig() // ���� ���ѽð�
    {
        if (!um.isWin)
        {
            if ((int)um.timeRemainig == 0)
            {
                um.isGameOver = true;
                um.gameover.SetActive(true);
                IncorrectPipe();
            }
            else
            {
                um.timeRemainig -= Time.deltaTime;
                success.text = "Time:  " + (int)um.timeRemainig;
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

        if (correctPipes == totalPipes) //�¸� ����
        {
            um.isWin = true;
            //if (gm.scenenum == 3 || gm.scenenum == 4)
            //    EnemyLevel.enemylv.ODaeGi2();

            SceneCheck();
            gm.puzzleLevel += 1;

            Invoke("ClosePipe", 1f);
            gm.nowpuzzle = false;
           
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        um.isWin = false;
        DataManager.instance.SaveData();
        canvas.gameObject.SetActive(false);
        
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].transform.rotation = pipesRot[i];
        }
        correctPipes = 0;
    }
    void IncorrectPipe()
    {
        canvas.gameObject.SetActive(false);

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].transform.rotation = pipesRot[i];
        }
        correctPipes = 0;
    }

    void SceneCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (gm.puzzleLevel == 1)
            {
                items[0].SetActive(true);
                items[1].SetActive(true);
                gm.ItemActive();
                GuideLineTxt.instance.SetDifferentTxt(3);
            }

            if (gm.puzzleLevel == 7)
            {
                items[2].SetActive(true);
                items[3].SetActive(true);
                mapPW.SetActive(true);
                gm.ItemActive();
                SubtitleCheck();
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if(gm.puzzleLevel == 3)
            {
                doorOpen.Invoke();
                SubtitleCheck();
            }
        }
    }
    public void SubtitleCheck()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(11);
                break;
            case 2:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(12);
                break;
            case 3:
                GuideLineTxt.instance.SetDifferentTxt(9);
                break;
                    case 4:
                GuideLineTxt.instance.SetDifferentTxt(9);
                break;
        }
    }
}
