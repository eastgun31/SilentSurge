using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject success;

    public GameObject PipesHolder; //파이프를 포함한 부모 오브젝트
    public GameObject[] Pipes; 

    [SerializeField]
    int totalPipes = 0; // 전체 파이프 수
    [SerializeField]
    int correctPipes = 0; // 올바르게 배치된 파이프 수


    void Start()
    {
        //전체 파이프수를 설정하고 Pipes배열에 저장

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

        if (correctPipes == totalPipes) //승리 조건
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
