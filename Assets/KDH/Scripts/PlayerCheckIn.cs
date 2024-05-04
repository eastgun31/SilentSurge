using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerCheckIn : MonoBehaviour
{
    public UnityEvent p_incheck;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")&&Input.GetKeyDown(KeyCode.Space))
        {
            p_incheck.Invoke();
        }
    }
}
