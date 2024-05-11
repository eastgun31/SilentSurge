using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTarget : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&GameManager.instance.puzzleLevel == 2)
        {
            GuideLineTxt.instance.SetDifferentTxt(21);
            this.gameObject.SetActive(false);
        }
    }
}
