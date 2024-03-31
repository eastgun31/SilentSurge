using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject success;

    public GameObject PipesHoldert;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHoldert.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHoldert.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        correctPipes += 1;

        Debug.Log("correct");

        if (correctPipes == totalPipes)
        {
            Debug.Log("Win");
            success.SetActive(true);
        }
    }

    public void WrongMove()
    {
        correctPipes -= 1;
    }
}
