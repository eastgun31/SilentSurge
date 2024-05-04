using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

public class EnterPuzzle : MonoBehaviour
{
    public int level;
    public GameObject puzzle;
    //public delegate void Puzzlechain(float a);
    GameManager gm;
    UiManager uiManager;

    private void Start()
    {
        gm = GameManager.instance;
        uiManager = UiManager.instance;
    }


    public int PuzzleActive1()
    {
        return gm.puzzleLevel switch
        {
            1 when level == 1 => uiManager.ActivePipeFst(), 
            2 when level == 2 => uiManager.ActiveSinFst(),
            3 when level == 3 => uiManager.ActiveHackingFst(),
            4 when level == 4 && gm.scenenum == 1 => uiManager.ActivePipeKeypadNumEight(),
            4 when level == 4 && gm.scenenum == 2 => uiManager.ActivePipeKeypadNumNine(),
            5 when level == 5 && gm.scenenum == 1 => uiManager.ActivePipeKeypadNumThree(),
            5 when level == 5 && gm.scenenum == 2 => uiManager.ActivePipeKeypadNumSix(),
            6 when level == 6 && gm.scenenum == 1 => uiManager.ActivePipeKeypadNumTwo(),
            6 when level == 6 && gm.scenenum == 2 => uiManager.ActivePipeKeypadNumThree(),
            7 when level == 7 && gm.scenenum == 1 => uiManager.ActivePipeKeypadNumFour(),
            7 when level == 7 && gm.scenenum == 2 => uiManager.ActivePipeKeypadNumFive(),
            8 when level == 8 => uiManager.ActiveKeypad(),
            _ => 0
        };
    }

    public int PuzzleActive2()
    {
        return gm.puzzleLevel switch
        {
            1 when level == 1 => uiManager.ActiveHackingFst(),
            2 when level == 2 => uiManager.ActiveKeypad(),
            3 when level == 3 => uiManager.ActivePipeSec(),
            _ => 0
        };
    }

    //public int PuzzleActive2()
    //{
    //    //gm.nowpuzzle = true;

    //    //return gm.puzzleLevel switch
    //    //{

    //    //};
    //}

    public int PipePuzzle1()
    {
        GameManager.instance.nowpuzzle = true;
        UiManager.instance.ActivePipeFst();
        return 0;
    }

    public int SinPuzzle()
    {
        GameManager.instance.nowpuzzle = true;
        uiManager.ActiveSinFst();
        return 0;
    }

    public int HackingPuzzle()
    {

        GameManager.instance.nowpuzzle = true;
        uiManager.ActiveHackingFst();
        return 0;
    }
    
    public int HintPuzzle1()
    {
        GameManager.instance.nowpuzzle = true;
        uiManager.ActivePipeKeypadNumEight();
        return 0;
    }

    public int HintPuzzle2()
    {
        GameManager.instance.nowpuzzle = true;
        uiManager.ActivePipeKeypadNumThree();
        return 0;
    }

    public int HintPuzzle3()
    {
        GameManager.instance.nowpuzzle = true;
        uiManager.ActivePipeKeypadNumTwo();
        return 0;
    }

    public int HintPuzzle4()
    {
        GameManager.instance.nowpuzzle = true;
        uiManager.ActivePipeKeypadNumFour();
        return 0;
    }

    public int LastPuzzle()
    {
        if (EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
        {
            GameManager.instance.nowpuzzle = true;
            UiManager.instance.ActiveKeypad();
            return 0;
        }
        else
            return 0;
    }
    public void SamplePuzzle()
    {
        if (GameManager.instance.puzzleLevel == 9 && level == 9)
        {
            puzzle.SetActive(true);
        }
        else
            return;
    }

}

//1 => uiManager.ActivePipeFst(),
//2 => uiManager.ActiveSinFst(),
//3 => uiManager.ActiveHackingFst(),
//4 when gm.scenenum == 1 => uiManager.ActivePipeKeypadNumEight(),
//4 when gm.scenenum == 2 => uiManager.ActivePipeKeypadNumNine(), 
//5 when gm.scenenum == 1 => uiManager.ActivePipeKeypadNumThree(),
//5 when gm.scenenum == 2 => uiManager.ActivePipeKeypadNumSix(),
//6 when gm.scenenum == 1 => uiManager.ActivePipeKeypadNumTwo(),
//6 when gm.scenenum == 2 => uiManager.ActivePipeKeypadNumThree(),
//7 when gm.scenenum == 1 => uiManager.ActivePipeKeypadNumFour(),
//7 when gm.scenenum == 2 => uiManager.ActivePipeKeypadNumFive(),
//8 => uiManager.ActiveKeypad()
