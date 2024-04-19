using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject clear;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            clear.SetActive(true);

    }

}
