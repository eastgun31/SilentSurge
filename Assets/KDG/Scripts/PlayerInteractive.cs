using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static Door_Parent;

public class PlayerInteractive : MonoBehaviour
{
    //public UnityEvent playpuzzle;
    Player player;
    EnterPuzzle enterPuzzle;
    PlayerView playerView;
    Door_Parent doort;
    DoorHandle_1 handle;

    string[] interactiveList = { "Door", "Bent", "Puzzle", "Cabinet" };

    //private int index = 0;

    private CapsuleCollider _pc;
    private Cabinet cabinet;
    private Vent vent;

    private void Start()
    {
        player = GetComponent<Player>();
        _pc = player.GetComponent<CapsuleCollider>();
        playerView = GetComponent<PlayerView>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (other.CompareTag(interactiveList[0]))
            {
                    doort = other.GetComponentInParent<Door_Parent>();
                    handle = other.GetComponent<DoorHandle_1>();
                    if (doort.nDoor == 0 && GameManager.instance.puzzleLevel >= 2)
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
                        doort.oDoor();
                    }
                    else if (doort.nDoor == 1 && GameManager.instance.puzzleLevel >= 3 )
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
                        doort.oDoor();
                    }
                    else if (doort.nDoor == 2 && GameManager.instance.puzzleLevel >= 4)
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
                        doort.oDoor();
                    }
            }
            else if (other.CompareTag(interactiveList[1]))
            {
                vent = other.GetComponentInParent<Vent>();
                if (other.name == ("Vent1") && vent.v1activate)
                {
                    player.transform.position = vent.vent2.transform.position;
                    vent.v1activate = false;
                    vent.ventActivate = true;
                }
                else if (other.name == ("Vent2") && vent.v2activate && vent.ventActivate)
                {
                    player.transform.position = vent.vent1.transform.position;
                    vent.v2activate = false;
                }
            }
            else if (other.CompareTag(interactiveList[2]) )
            {
                //playpuzzle.Invoke();
                player.state = Player.PlayerState.puzzling;
                player.velocity = Vector3.zero;
                enterPuzzle = other.GetComponent<EnterPuzzle>();
                player.RunOff();

                switch (enterPuzzle.level)
                {
                    case 1:
                        enterPuzzle.PipePuzzle1();
                        break;
                    case 2:
                        enterPuzzle.SinPuzzle();
                        break;
                    case 3:
                        enterPuzzle.HackingPuzzle();
                        break;
                    case 4:
                        enterPuzzle.HintPuzzle1();
                        break;
                    case 5:
                        enterPuzzle.HintPuzzle2();
                        break;
                    case 6:
                        enterPuzzle.HintPuzzle3();
                        break;
                    case 7:
                        enterPuzzle.HintPuzzle4();
                        break;
                    case 8:
                        enterPuzzle.LastPuzzle();
                        break;
                    case 9:
                        enterPuzzle.SamplePuzzle();
                        break;
                }
            }
            else if (other.CompareTag(interactiveList[3]) && EnemyLevel.enemylv.LvStep != EnemyLevel.ELevel.level3)
            {
                cabinet = other.GetComponentInParent<Cabinet>();
                if (!GameManager.instance.isHide)
                {
                    player.velocity = Vector3.zero;
                    player.transform.position = cabinet.hidePoints.transform.position;
                    GameManager.instance.isHide = true;
                    _pc.isTrigger = true;
                    playerView.viewAngle = 360;
                    playerView.viewRadius = 2f;
                }
                else
                {
                    player.transform.position = cabinet.idlePoints.transform.position;
                    GameManager.instance.isHide = false;
                    _pc.isTrigger = false;
                    playerView.viewAngle = GameManager.instance.playerviewA;
                    playerView.viewRadius = GameManager.instance.playerviewR;
                }
            }
            //else if(세이브 포인트 검사)
            else
                return;
        }
    }
    //IEnumerator PInteractive(Collider other)
    //{
    //    while (true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            if (other.CompareTag(interactiveList[0]))
    //            {
    //                doort = other.GetComponentInParent<Door_Parent>();
    //                handle = other.GetComponent<DoorHandle_1>();
    //                if (doort.nDoor == 0)
    //                {
    //                    if (handle.Doorindex == 1)
    //                    {
    //                        doort.PlayerPos_1 = true;
    //                        doort.PlayerPos_0 = false;
    //                    }
    //                    else if (handle.Doorindex == 0)
    //                    {
    //                        doort.PlayerPos_1 = false;
    //                        doort.PlayerPos_0 = true;
    //                    }
    //                    doort.oDoor();
    //                }
    //                else if (doort.nDoor == 1 && GameManager.instance.puzzleLevel >= 3 )
    //                {
    //                    if (handle.Doorindex == 1)
    //                    {
    //                        doort.PlayerPos_1 = true;
    //                        doort.PlayerPos_0 = false;
    //                    }
    //                    else if (handle.Doorindex == 0)
    //                    {
    //                        doort.PlayerPos_1 = false;
    //                        doort.PlayerPos_0 = true;
    //                    }
    //                    doort.oDoor();
    //                }
    //                else if (doort.nDoor == 2 && GameManager.instance.puzzleLevel >= 4)
    //                {
    //                    if (handle.Doorindex == 1)
    //                    {
    //                        doort.PlayerPos_1 = true;
    //                        doort.PlayerPos_0 = false;
    //                    }
    //                    else if (handle.Doorindex == 0)
    //                    {
    //                        doort.PlayerPos_1 = false;
    //                        doort.PlayerPos_0 = true;
    //                    }
    //                    doort.oDoor();
    //                }
    //            }
    //            else if (other.CompareTag(interactiveList[1]))
    //            {
    //                vent = other.GetComponentInParent<Vent>();
    //                if (other.name == ("Vent1") && vent.v1activate)
    //                {
    //                    player.transform.position = vent.vent2.transform.position;
    //                    vent.v1activate = false;
    //                    vent.ventActivate = true;
    //                }
    //                else if (other.name == ("Vent2") && vent.v2activate && vent.ventActivate)
    //                {
    //                    player.transform.position = vent.vent1.transform.position;
    //                    vent.v2activate = false;
    //                }
    //            }
    //            else if (other.CompareTag(interactiveList[2]))
    //            {
    //                //playpuzzle.Invoke();
    //                player.state = Player.PlayerState.puzzling;
    //                player.velocity = Vector3.zero;
    //                enterPuzzle = other.GetComponent<EnterPuzzle>();
    //                SoundManager.instance.EffectOff();

    //                switch (enterPuzzle.level)
    //                {
    //                    case 1:
    //                        enterPuzzle.PipePuzzle1();
    //                        break;
    //                    case 2:
    //                        enterPuzzle.SinPuzzle();
    //                        break;
    //                    case 3:
    //                        enterPuzzle.HackingPuzzle();
    //                        break;
    //                    case 4:
    //                        enterPuzzle.HintPuzzle1();
    //                        break;
    //                    case 5:
    //                        enterPuzzle.HintPuzzle2();
    //                        break;
    //                    case 6:
    //                        enterPuzzle.HintPuzzle3();
    //                        break;
    //                    case 7:
    //                        enterPuzzle.HintPuzzle4();
    //                        break;
    //                    case 8:
    //                        enterPuzzle.LastPuzzle();
    //                        break;
    //                }
    //            }
    //            else if (other.CompareTag(interactiveList[3]) && EnemyLevel.enemylv.LvStep != EnemyLevel.ELevel.level3)
    //            {
    //                cabinet = other.GetComponentInParent<Cabinet>();
    //                if (!GameManager.instance.isHide)
    //                {
    //                    player.velocity = Vector3.zero;
    //                    player.transform.position = cabinet.hidePoints.transform.position;
    //                    GameManager.instance.isHide = true;
    //                    _pc.isTrigger = true;
    //                    playerView.viewAngle = 360;
    //                    playerView.viewRadius = 2f;
    //                }
    //                else
    //                {
    //                    player.transform.position = cabinet.idlePoints.transform.position;
    //                    GameManager.instance.isHide = false;
    //                    _pc.isTrigger = false;
    //                    playerView.viewAngle = 100;
    //                    playerView.viewRadius = 5f;
    //                }
    //            }
    //        }

    //        yield return null;
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    StartCoroutine(PInteractive(other));
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    StopCoroutine(PInteractive(other));
    //}

}
