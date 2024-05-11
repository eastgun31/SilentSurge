using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoServer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&& GameManager.instance.puzzleLevel >= 3)
        {
            GuideLineTxt.instance.SetDifferentTxt7();
            this.gameObject.SetActive(false);
        }
    }
}