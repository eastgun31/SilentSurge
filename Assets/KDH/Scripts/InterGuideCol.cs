using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterGuideCol : MonoBehaviour
{
    public GameObject guideCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            guideCol.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            guideCol.SetActive(false);
    }
}
