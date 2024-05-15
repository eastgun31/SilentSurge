using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombsetup : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GuideLineTxt.instance.SetDifferentTxt(5);
            this.gameObject.SetActive(false);
        }
    }
}
