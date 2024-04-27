using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorHandle_1 : MonoBehaviour, IDoor   // 문 손잡이 2
{
    public bool canOpen = true;
    public bool isOpen = false;
    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;
    public Door_Parent tDoor { get; set; }
    public GameObject P_Door;

    public int Doorindex = 0;

    [SerializeField]
    private Material mat_Door;
    [SerializeField]
    private Material mat_Outline;
    [SerializeField]
    private Material mat_NoOutline;

    private void Awake()
    {
        tDoor = P_Door.GetComponent<Door_Parent>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(tDoor.nDoor == 0 || tDoor.nDoor == 1 && GameManager.instance.puzzleLevel >= 3 || tDoor.nDoor == 2 && GameManager.instance.puzzleLevel >= 4)
                P_Door.GetComponent<MeshRenderer>().material = mat_Outline;  // 아웃라인 메테리얼로 0번 배열 변경 
            else if(tDoor.nDoor == 1 && GameManager.instance.puzzleLevel < 3 || tDoor.nDoor == 2 && GameManager.instance.puzzleLevel < 4)
                P_Door.GetComponent<MeshRenderer>().material = mat_NoOutline;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            P_Door.GetComponent<MeshRenderer>().material = mat_Door;  // 문 기본 메테리얼로 0번 배열 변경
        }
        tDoor.PlayerPos_0 = false;
        tDoor.PlayerPos_1 = false;
    }
}
