using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractive : MonoBehaviour
{
    public UnityEvent playpuzzle;
    Player player;

    string[] interactiveList = { "Door", "Bent", "Puzzle", "Cabinet" };

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void puzzle1()
    {
        if (GameManager.instance.puzzleLevel == 1)
        {

        }
        else
            return;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(interactiveList[0]) && Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.GetComponent<TestDoor>();
            
        }
        else if (other.CompareTag(interactiveList[1]) && Input.GetKeyDown(KeyCode.Space))
        {

        }
        else if (other.CompareTag(interactiveList[2]) && Input.GetKeyDown(KeyCode.Space))
        {
            playpuzzle.Invoke();
            player.state = Player.PlayerState.puzzling;
        }
        else
            return;
    }
}
