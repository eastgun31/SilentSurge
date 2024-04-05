using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeManager : MonoBehaviour
{

    public GameObject pipesHolder; //�������� ������ �θ� ������Ʈ
    public GameObject[] pipes; 

    [SerializeField]
    int totalPipes = 0; // ��ü ������ ��
    [SerializeField]
    int correctPipes = 0; // �ùٸ��� ��ġ�� ������ ��

    Player.PlayerState player;

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

       // Debug.Log("correct");

        if (correctPipes == totalPipes) //�¸� ����
        {
            //Debug.Log("Win");
            UiManager.instance.isWin = true;
            GameManager.instance.puzzleLevel += 1;
            player = Player.PlayerState.idle;
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
        UiManager.instance.ClosePipeSec();
    }
}
