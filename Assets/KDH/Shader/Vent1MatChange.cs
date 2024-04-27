using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent1MatChange : MonoBehaviour
{
    public GameObject matVent;


    [SerializeField]
    private Vent vent;

    [SerializeField]
    private Material mat_Original;
    [SerializeField]
    private Material mat_Outline;
    [SerializeField]
    private Material mat_NoOutline;

    private void Start()
    {
        vent = transform.parent.GetComponent<Vent>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (vent.v1activate)
                matVent.GetComponent<MeshRenderer>().material = mat_Outline;
            else
                matVent.GetComponent<MeshRenderer>().material = mat_NoOutline;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        matVent.GetComponent<MeshRenderer>().material = mat_Original;
    }
}
