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
    
    public void HintPuzzle1()
    {
        if (GameManager.instance.puzzleLevel == 4 && level == 4)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActivePipeKeypadNumEight();
        }
        else
            return;
    }

    public void HintPuzzle2()
    {
        if (GameManager.instance.puzzleLevel == 5 && level == 5)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActivePipeKeypadNumThree();
        }
        else
            return;
    }

    public void HintPuzzle3()
    {
        if (GameManager.instance.puzzleLevel == 6 && level == 6)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActivePipeKeypadNumTwo();
        }
        else
            return;
    }

    public void HintPuzzle4()
    {
        if (GameManager.instance.puzzleLevel == 7 && level == 7)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActivePipeKeypadNumFour();
        }
        else
            return;
    }

    public void LastPuzzle()
    {
        if (GameManager.instance.puzzleLevel == 8 && level == 8)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActiveKeypad();
        }
        else
            return;
    }

}
