using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static Door;

public class TInter : MonoBehaviour
{
    //public UnityEvent playpuzzle;
    Player player;
    EnterPuzzle enterPuzzle;
    PlayerView playerView;
    Door doort;
    DoorHandle handle;
    GameManager gm;

    string[] interactiveList = { "Door", "Bent", "Puzzle", "Cabinet" };

    //private int index = 0;
    Ray ray;

    private CapsuleCollider _pc;
    private Cabinet cabinet;
    private Vent vent;

    private void Start()
    {
        gm = GameManager.instance;
        player = GetComponent<Player>();
        _pc = player.GetComponent<CapsuleCollider>();
        playerView = GetComponent<PlayerView>();
    }
    public void InterT()
    {
        //switch(interactiveList)
        //{ 

        //}
    }


    public void InteractiveObj(RaycastHit phit)
    {
        var hit = phit.transform.gameObject;
        if (hit.CompareTag(interactiveList[0]))
        {
            doort = hit.GetComponentInParent<Door>();
            handle = hit.GetComponent<DoorHandle>();
            if (doort.nDoor == 0 && gm.puzzleLevel >= 2)
            {
                if (handle.Doorindex == 1)
                {
                    doort.PlayerPos_1 = true;
                    doort.PlayerPos_0 = false;
                }
                else if (handle.Doorindex == 0)
                {
                    doort.PlayerPos_1 = false;
                    doort.PlayerPos_0 = true;
                }
                doort.OpenDoor();
            }
            else if (doort.nDoor == 1 && gm.puzzleLevel >= 3)
            {
                if (handle.Doorindex == 1)
                {
                    doort.PlayerPos_1 = true;
                    doort.PlayerPos_0 = false;
                }
                else if (handle.Doorindex == 0)
                {
                    doort.PlayerPos_1 = false;
                    doort.PlayerPos_0 = true;
                }
                doort.OpenDoor();
            }
            else if (doort.nDoor == 2 && gm.puzzleLevel >= 4)
            {
                if (handle.Doorindex == 1)
                {
                    doort.PlayerPos_1 = true;
                    doort.PlayerPos_0 = false;
                }
                else if (handle.Doorindex == 0)
                {
                    doort.PlayerPos_1 = false;
                    doort.PlayerPos_0 = true;
                }
                doort.OpenDoor();
            }
        }
        else if (hit.CompareTag(interactiveList[1]))
        {
            vent = hit.GetComponentInParent<Vent>();
            if (hit.name == ("Vent1") && vent.v1activate)
            {
                player.transform.position = vent.vent2.transform.position;
                vent.v1activate = false;
                vent.ventActivate = true;
            }
            else if (hit.name == ("Vent2") && vent.v2activate && vent.ventActivate)
            {
                player.transform.position = vent.vent1.transform.position;
                vent.v2activate = false;
            }
        }
        if (hit.CompareTag(interactiveList[2]))
        {
            //playpuzzle.Invoke();
            player.state = Player.PlayerState.puzzling;
            player.velocity = Vector3.zero;
            enterPuzzle = hit.GetComponent<EnterPuzzle>();
            player.RunOff();

            if (gm.scenenum == 1 || gm.scenenum == 2)
            {
                enterPuzzle.PuzzleActive1();
            }
        }
        else if (hit.CompareTag(interactiveList[3]) && EnemyLevel.enemylv.LvStep != EnemyLevel.ELevel.level3)
        {
            cabinet = hit.GetComponentInParent<Cabinet>();
            if (!gm.isHide)
            {
                player.velocity = Vector3.zero;
                player.transform.position = cabinet.hidePoints.transform.position;
                gm.isHide = true;
                _pc.isTrigger = true;
                playerView.viewAngle = 360;
                playerView.viewRadius = 2f;
            }
            else
            {
                player.transform.position = cabinet.idlePoints.transform.position;
                gm.isHide = false;
                _pc.isTrigger = false;
                playerView.viewAngle = gm.playerviewA;
                playerView.viewRadius = gm.playerviewR;
            }
        }
        //else if(세이브 포인트 검사)
        else
            return;
    }
}
