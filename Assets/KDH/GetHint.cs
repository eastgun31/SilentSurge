using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHint : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.instance.puzzleLevel < 5)
        {
            GuideLineTxt.instance.SetDifferentTxt9();
            this.gameObject.SetActive(false);
        }
    }
}
