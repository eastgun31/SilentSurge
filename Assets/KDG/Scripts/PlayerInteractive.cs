using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    string[] interactiveList = { "Door", "Bent", "Puzzle", "Cabinet" };

    void puzzle1()
    {
        if (GameManager.instance.puzzleLevel == 1)
        {

        }
        else
            return;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.other.CompareTag(interactiveList[0]))
        {

        }
        else if (collision.other.CompareTag(interactiveList[1]))
        {

        }
        else if (collision.other.CompareTag(interactiveList[2]))
        {

        }
        else
            return;
    }
}
