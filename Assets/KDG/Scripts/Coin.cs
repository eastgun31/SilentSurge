using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class Coin : MonoBehaviour, IItem
{
    public int value { get; set; }
    public GameObject coin;
    Item itemvalues = new Item();

    Player player;

    void Start()
    {
        itemvalues.count = 5;
        value = 2;
    }

    public void GetItem()
    {
        Debug.Log("ƒ⁄¿Œ»πµÊ");
        GameManager.instance.itemcheck[1] = true;
        //player = gameObject.GetComponent<Player>();
        //player.itemGet[1] = true;
    }
    public void ItemCharge()
    {
        GameManager.instance.itemcount[1] = itemvalues.count;
    }
}
