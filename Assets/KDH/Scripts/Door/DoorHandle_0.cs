using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle_0 : MonoBehaviour
{
    public bool canOpen = true;
    public bool isOpen = false;
    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;
    public Door_Parent tDoor;
    public GameObject P_Door;

    private void Awake()
    {
        tDoor = P_Door.GetComponent<Door_Parent>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tDoor.PlayerPos_0 = true;
            tDoor.PlayerPos_1 = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tDoor.PlayerPos_0 = false;
            tDoor.PlayerPos_1 = false;
        }
    }

}
