using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorrOpen_Test : MonoBehaviour
{
    public GameObject test;
    public GameObject bt;
    public GameObject aaa;

    public void Bt_Click()
    {
        test.SetActive(false);
        bt.SetActive(false);
        aaa.SetActive(false);
    }
}
