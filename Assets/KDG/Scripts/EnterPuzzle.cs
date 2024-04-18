using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPuzzle : MonoBehaviour
{
    public int level;

    public void PipePuzzle1()
    {
        if (GameManager.instance.puzzleLevel == 1 && level == 1)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActivePipeFst();
        }
        else
            return;

    }

    public void SinPuzzle()
    {
        if (GameManager.instance.puzzleLevel == 2 && level ==2)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActiveSinFst();
        }
        else
            return;
    }

    public void HackingPuzzle()
    {
        if (GameManager.instance.puzzleLevel == 3 && level == 3)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActiveHackingFst();
        }
        else
            return;
    }

}
