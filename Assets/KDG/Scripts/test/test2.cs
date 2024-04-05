using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public void Keypad()
    {
        if (GameManager.instance.puzzleLevel == 2)
        {
            UiManager.instance.ActiveSinFst();
        }
        else
            return;
    }
}
