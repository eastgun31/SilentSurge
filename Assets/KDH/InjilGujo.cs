using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjilGujo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&& !GuideLineTxt.instance.isBossOn)
        {
            GuideLineTxt.instance.SetDifferentTxt(5);
            this.gameObject.SetActive(false);
        }

    }
}
