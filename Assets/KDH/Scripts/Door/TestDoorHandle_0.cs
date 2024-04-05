using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoorHandle_0 : MonoBehaviour
{
    public bool canOpen = true;
    public TestDoor testDoor;
    public GameObject P_Door;

    private void Awake()
    {
        testDoor = P_Door.GetComponent<TestDoor>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            testDoor.PlayerPos_0 = true;
            testDoor.PlayerPos_1 = false;
        }
    }
}
