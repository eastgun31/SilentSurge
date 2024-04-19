using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChange : MonoBehaviour
{
    [SerializeField]
    private Material mat_Original;
    [SerializeField]
    private Material mat_Outline;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().material = mat_Outline;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GetComponent<MeshRenderer>().material=mat_Original;
    }
}
