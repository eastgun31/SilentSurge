using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle_1 : MonoBehaviour
{
    public bool canOpen = true;
    public bool isOpen = false;
    public bool PlayerPos_0 = false;
    public bool PlayerPos_1 = false;
    public Door_Parent tDoor;
    public GameObject P_Door;
    public DoorMaterialChange dMatchange;
    public GameObject dMatC;

    private void Awake()
    {
        tDoor = P_Door.GetComponent<Door_Parent>();
        dMatchange = dMatC.GetComponent<DoorMaterialChange>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            tDoor.PlayerPos_0 = false;
            tDoor.PlayerPos_1 = true;
            dMatchange.OutlineMat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tDoor.PlayerPos_0 = false;
            tDoor.PlayerPos_1 = false;
            dMatchange.DoorMat();
        }
    }
}
