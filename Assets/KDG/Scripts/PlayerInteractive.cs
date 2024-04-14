using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Door_Parent;

public class PlayerInteractive : MonoBehaviour
{
    //public UnityEvent playpuzzle;
    Player player;
    EnterPuzzle enterPuzzle;

    TestHandle testHandle;

    Door_Parent doort;

    string[] interactiveList = { "Door", "Bent", "Puzzle", "Cabinet" };

    private int index = 0;

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
            doort = other.GetComponentInParent<Door_Parent>();
            if (other.GetComponent<DoorHandle_1>().Doorindex == 1)
            {
                doort.PlayerPos_1 = true;
                doort.PlayerPos_0 = false;
            }
            else if (other.GetComponent<DoorHandle_1>().Doorindex == 0)
            {
                doort.PlayerPos_1 = false;
                doort.PlayerPos_0 = true;
            }
            doort.oDoor();
        }
        else if (other.CompareTag(interactiveList[1]) && Input.GetKeyDown(KeyCode.Space))
        {

        }
        else if (other.CompareTag(interactiveList[2]) && Input.GetKeyDown(KeyCode.Space))
        {
            //playpuzzle.Invoke();
            player.state = Player.PlayerState.puzzling;
            player.velocity = Vector3.zero;
            enterPuzzle = other.GetComponent<EnterPuzzle>();
            if (enterPuzzle.level == 1)
            {
                enterPuzzle.PipePuzzle1();
            }
            else if (enterPuzzle.level == 2)
            {
                enterPuzzle.Keypad();
            }
        }
        else if (other.CompareTag(interactiveList[3]) && Input.GetKeyDown(KeyCode.Space))
        {
            //this.transform.position= other.transform.position;
            //if (!GameManager.instance.isHide)
            //{
            //    player.state = Player.PlayerState.hide;
            //    this.transform.position = other.transform.position;
            //    GameManager.instance.isHide = true;
            //}
            //else
            //{
            //    player.state = Player.PlayerState.idle;
            //    GameManager.instance.isHide = false;
            //}
        }
        //else if(세이브 포인트 검사)
        else
            return;
    }

}
