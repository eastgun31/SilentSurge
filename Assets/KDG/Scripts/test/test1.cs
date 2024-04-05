using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test1 : MonoBehaviour
{

    public void PipePuzzle1()
    {
        if (GameManager.instance.puzzleLevel == 1)
        {
            UiManager.instance.ActivePipeFst();
        }
        else
            return;
        
    }
}
