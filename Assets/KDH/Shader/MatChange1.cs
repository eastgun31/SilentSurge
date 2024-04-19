using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange1 : MonoBehaviour
{
    public GameObject matCabinet;

    [SerializeField]
    private Material mat_Original;
    [SerializeField]
    private Material mat_Outline;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            matCabinet.GetComponent<MeshRenderer>().material = mat_Outline;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        matCabinet.GetComponent<MeshRenderer>().material=mat_Original;
    }
}
