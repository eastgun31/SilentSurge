using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject timeText;

    public GameObject pipesHolder; //�������� ������ �θ� ������Ʈ
    public GameObject[] pipes;

    public GameObject[] items;

    [SerializeField]
    int totalPipes = 0; // ��ü ������ ��
    [SerializeField]
    int correctPipes = 0; // �ùٸ��� ��ġ�� ������ ��

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
        UiManager.instance.TimeRemainig();
    }

    public void CorrectMove()
    {
        correctPipes += 1;

        if (correctPipes == totalPipes) //�¸� ����
        {
            UiManager.instance.isWin = true;
            GameManager.instance.puzzleLevel += 1;
            GameManager.instance.nowpuzzle = false;

            if (GameManager.instance.puzzleLevel == 2)
            {
                items[0].SetActive(true);
                items[1].SetActive(true);
            }
            
            Invoke("ClosePipe", 2f);
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        canvas.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }
}
