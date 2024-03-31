using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class Coin : MonoBehaviour, IItem
{
    public int value { get; set; }
    public GameObject coin;

    void Start()
    {
        value = 2;
    }

    void Update()
    {

    }
    public void UseItem()
    {
        Debug.Log("코인사용");
    }
    public void ItemCharge()
    {

    }
}
