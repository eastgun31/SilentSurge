using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSeojae : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GuideLineTxt.instance.SetDifferentTxt(19);
            this.gameObject.SetActive(false);
        }
    }
}
