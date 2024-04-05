using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public GameObject pipePuzzle1;

    public void PipePuzzle1()
    {
        if (GameManager.instance.puzzleLevel == 1)
        {
            pipePuzzle1.SetActive(true);
        }
        else
            return;
        
    }
}
