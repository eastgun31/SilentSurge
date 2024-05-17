using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JeungerPassGuide : MonoBehaviour
{
    public UnityEvent guide3Pass;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            guide3Pass.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
