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
    public void GetItem()
    {
        Debug.Log("ƒ⁄¿Œ»πµÊ");
        GameManager.instance.itemcheck[1] = true;
    }
    public void ItemCharge()
    {

    }
}
