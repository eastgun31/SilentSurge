using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFirst : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GuideLineTxt.instance.SetDifferentTxt(20);
            this.gameObject.SetActive(false);
        }
    }
}
