using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public GameObject keypad;

    public void Keypad()
    {
        if (GameManager.instance.puzzleLevel == 2)
        {
            keypad.SetActive(true);
        }
        else
            return;
    }
}
