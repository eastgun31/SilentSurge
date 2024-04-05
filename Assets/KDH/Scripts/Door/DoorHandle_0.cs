using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle_0 : MonoBehaviour
{
    public bool canOpen = true;
    public Door tDoor;
    public GameObject P_Door;

    private void Awake()
    {
        tDoor = P_Door.GetComponent<Door>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
          tDoor.PlayerPos_0 = true;
          tDoor.PlayerPos_1 = false;
        }
    }
}
