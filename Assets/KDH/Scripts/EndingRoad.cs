using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingRoad : MonoBehaviour
{
    [SerializeField]
    GameObject etp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            other.transform.position=etp.transform.position;
        }
    }
}
