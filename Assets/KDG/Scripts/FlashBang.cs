using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class FlashBang : MonoBehaviour, IItem
{
    public int value { get; set; }

    void Start()
    {
        value = 3;
    }

    void Update()
    {

    }
    public void UseItem()
    {
        Debug.Log("¼¶±¤Åº»ç¿ë");
    }
    public void ItemCharge()
    {

    }
}
