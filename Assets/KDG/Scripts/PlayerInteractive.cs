using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    string[] interactiveList = { "Door", "Bent", "Puzzle" };

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
