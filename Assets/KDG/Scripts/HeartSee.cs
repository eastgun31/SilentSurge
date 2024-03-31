using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class HeartSee : MonoBehaviour, IItem
{
    public int value { get; set; }

    void Start()
    {
        value = 4;
    }

    void Update()
    {

    }
    public void UseItem()
    {
        Debug.Log("심박측정기사용");
    }
    public void ItemCharge()
    {

    }
}
