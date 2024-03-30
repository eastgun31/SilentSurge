using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;
using System.Threading;

public class HandGun : MonoBehaviour, IItem
{
    public int value {  get; set; }

    void Start()
    {
        value = 1;
    }

    void Update()
    {
        
    }
    public void UseItem()
    {
        Debug.Log("±ÇÃÑ»ç¿ë");
    }
    public void ItemCharge()
    {

    }
}
