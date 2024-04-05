using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractive : MonoBehaviour
{
    public UnityEvent playpuzzle;

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
        if (collision.other.CompareTag(interactiveList[0]) && Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.GetComponent<TestDoor>();
        }
        else if (collision.other.CompareTag(interactiveList[1]) && Input.GetKeyDown(KeyCode.Space))
        {

        }
        else if (collision.other.CompareTag(interactiveList[2]) && Input.GetKeyDown(KeyCode.Space))
        {
            playpuzzle.Invoke();
        }
        else
            return;
    }
}
