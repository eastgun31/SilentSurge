using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorHandle : MonoBehaviour
{
    public Door tDoor { get; set; }
    public GameObject P_Door;

    GameManager gm;

    public int Doorindex = 0;

    [SerializeField]
    private Material mat_Door;
    [SerializeField]
    private Material mat_Outline;
    [SerializeField]
    private Material mat_NoOutline;

    private void Awake()
    {
        tDoor = P_Door.GetComponent<Door>();
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            tDoor.PlayerPos_0 = true;
            if (GameManager.instance.scenenum == 1)
            {
                if (tDoor.nDoor == 0 && gm.puzzleLevel >= 2 || tDoor.nDoor == 1 && gm.puzzleLevel >= 3 || tDoor.nDoor == 2 && gm.puzzleLevel >= 4)
                    P_Door.GetComponent<MeshRenderer>().material = mat_Outline;  // 아웃라인 메테리얼로 0번 배열 변경 
                if (tDoor.nDoor == 0 && gm.puzzleLevel < 2 || tDoor.nDoor == 1 && gm.puzzleLevel < 3 || tDoor.nDoor == 2 && gm.puzzleLevel < 4)
                    P_Door.GetComponent<MeshRenderer>().material = mat_NoOutline;
            }
            else if (GameManager.instance.scenenum == 2)
            {
                if (tDoor.nDoor == 0 || tDoor.nDoor == 1 && gm.puzzleLevel >= 2 || tDoor.nDoor == 2 && gm.puzzleLevel >= 4)
                    P_Door.GetComponent<MeshRenderer>().material = mat_Outline;
                if (tDoor.nDoor == 1 && gm.puzzleLevel < 2 || tDoor.nDoor == 2 && gm.puzzleLevel < 4)
                    P_Door.GetComponent<MeshRenderer>().material = mat_NoOutline;
            }
            else if (GameManager.instance.scenenum == 3 || GameManager.instance.scenenum == 4)
            {
                if (tDoor.nDoor == 0 || tDoor.nDoor == 1 && gm.puzzleLevel >= 2 || tDoor.nDoor == 2 && gm.puzzleLevel >= 3)
                    P_Door.GetComponent<MeshRenderer>().material = mat_Outline;
                if (tDoor.nDoor == 1 && gm.puzzleLevel < 2 || tDoor.nDoor == 2 && gm.puzzleLevel < 3)
                    P_Door.GetComponent<MeshRenderer>().material = mat_NoOutline;
            }
            else if (GameManager.instance.scenenum == 5)
            {
                if (tDoor.nDoor == 0 || tDoor.nDoor == 1 && gm.puzzleLevel >= 2)
                    P_Door.GetComponent<MeshRenderer>().material = mat_Outline;
                if (tDoor.nDoor == 1 && gm.puzzleLevel < 2)
                    P_Door.GetComponent<MeshRenderer>().material = mat_NoOutline;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            P_Door.GetComponent<MeshRenderer>().material = mat_Door;  // 문 기본 메테리얼로 0번 배열 변경
        }
        tDoor.PlayerPos_0 = false;
    }
}
