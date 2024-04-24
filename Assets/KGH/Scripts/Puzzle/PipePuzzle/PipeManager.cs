using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    public GameObject canvas;

    public GameObject pipesHolder; //�������� ������ �θ� ������Ʈ
    public GameObject[] pipes;

    public GameObject[] items;

    [SerializeField]
    int totalPipes = 0; // ��ü ������ ��
    [SerializeField]
    int correctPipes = 0; // �ùٸ��� ��ġ�� ������ ��

    public Text success;

    void Start()
    {
        //��ü ���������� �����ϰ� Pipes�迭�� ����

        totalPipes = pipesHolder.transform.childCount;
        pipes = new GameObject[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
        }
    }
    private void Update()
    {
       TimeRemainig();
    }

    public void TimeRemainig() // ���� ���ѽð�
    {
        if (!UiManager.instance.isWin)
        {
            if ((int)UiManager.instance.timeRemainig == 0)
            {
                success.text = "FAIL";
                UiManager.instance.isGameOver = true;
                UiManager.instance.gameover.SetActive(true);
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
            Invoke("ResetTime", 2f);
        }
    }
    public void CorrectMove()
    {
        correctPipes += 1;

        if (correctPipes == totalPipes) //�¸� ����
        {
            UiManager.instance.isWin = true;
            
            

            if (GameManager.instance.puzzleLevel == 1)
            {
                items[0].SetActive(true);
                items[1].SetActive(true);
            }

            GameManager.instance.puzzleLevel += 1;

            DataManager.instance.SaveData();
            Invoke("ClosePipe", 2f);
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        GameManager.instance.nowpuzzle = false;
        UiManager.instance.isWin = false;
        canvas.gameObject.SetActive(false);
    }
}
