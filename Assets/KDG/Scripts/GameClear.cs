using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameClear : MonoBehaviour
{
    public GameObject clear;
    public int value;
    public UnityEvent lastAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && value == 1)
            clear.SetActive(true);
        else if(other.CompareTag("Player") && value == 2)
        {
            GameManager.instance.enemyDown = true;
            lastAction.Invoke();
        }
    }

}
