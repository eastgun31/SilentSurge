using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{

    public GameObject pipesHolder; //파이프를 포함한 부모 오브젝트
    public GameObject[] pipes; 

    [SerializeField]
    int totalPipes = 0; // 전체 파이프 수
    [SerializeField]
    int correctPipes = 0; // 올바르게 배치된 파이프 수

    void Start()
    {
        //전체 파이프수를 설정하고 Pipes배열에 저장

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

        Debug.Log("correct");

        if (correctPipes == totalPipes) //승리 조건
        {
            Debug.Log("Win");
            UiManager.instance.isWin = true;
            Invoke("ClosePipe", 2f);
            
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }

    public void ClosePipe()
    {
        UiManager.instance.ClosePipeFst();
    }
}
