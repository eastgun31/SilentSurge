using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoorHandle : MonoBehaviour
{
    public bool canOpen;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            canOpen = true;
            Debug.Log("문손잡이가 플레이어 감지");
        }
    }
}
