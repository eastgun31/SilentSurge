using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject success;

    public GameObject PipesHolder; //�������� ������ �θ� ������Ʈ
    public GameObject[] Pipes; 

    [SerializeField]
    int totalPipes = 0; // ��ü ������ ��
    [SerializeField]
    int correctPipes = 0; // �ùٸ��� ��ġ�� ������ ��


    void Start()
    {
        //��ü ���������� �����ϰ� Pipes�迭�� ����

        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        correctPipes += 1;

        Debug.Log("correct");

        if (correctPipes == totalPipes) //�¸� ����
        {
            Debug.Log("Win");
            success.SetActive(true);
            Invoke("ClosePipe", 2f);
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        Canvas.SetActive(false);
    }
}
