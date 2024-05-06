using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCheckOut : MonoBehaviour
{
    public UnityEvent p_outcheck;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            p_outcheck.Invoke();
        }
    }
}
