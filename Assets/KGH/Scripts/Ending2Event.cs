using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ending2Event : MonoBehaviour
{
    public GameObject car_P;
    public GameObject car_H;
    public GameObject run_P;
    public GameObject run_H;

    public GameObject carAnim;

    public void RunClosed()
    {
        run_P.SetActive(false);
        run_H.SetActive(false);
    }

    public void carActive()
    {
        car_P.SetActive(true);
        car_H.SetActive(true);
    }
    public void PlayAnim()
    {
        carAnim.GetComponent<DOTweenAnimation>().DOPlay();
    }
}
