using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoCCTV : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GuideLineTxt.instance.SetDifferentTxt(3);
            this.gameObject.SetActive(false);
        }
    }
}
