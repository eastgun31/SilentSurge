using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BDoor : MonoBehaviour
{
    public GameObject L_Bdoor;
    public GameObject R_Bdoor;

    public void BDoorOpen()
    {
        L_Bdoor.GetComponent<DOTweenAnimation>().DOPlay();
        R_Bdoor.GetComponent<DOTweenAnimation>().DOPlay();
    }
}
