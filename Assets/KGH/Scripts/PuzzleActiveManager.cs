using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActiveManager : MonoBehaviour
{
    public GameObject keypad;

    public GameObject pipePuzzle1;
    public GameObject pipePuzzle2;

    public void PipePuzzle1()
    {
        pipePuzzle1.SetActive(true);
    }
    public void PipePuzzle2()
    {
        pipePuzzle2.SetActive(true);
    }
    public void Keypad()
    {
        keypad.SetActive(true);
    }
}
