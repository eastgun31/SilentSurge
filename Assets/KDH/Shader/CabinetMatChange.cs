using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetMatChange : MonoBehaviour
{
    public GameObject matCabinet;

    [SerializeField]
    private Material mat_Original;
    [SerializeField]
    private Material mat_Outline;
    [SerializeField]
    private Material mat_NoOutline;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
                matCabinet.GetComponent<MeshRenderer>().material = mat_NoOutline;
            else
            matCabinet.GetComponent<MeshRenderer>().material = mat_Outline;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        matCabinet.GetComponent<MeshRenderer>().material=mat_Original;
    }
}
