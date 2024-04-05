using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class Armor : MonoBehaviour, IItem
{
    public int value { get; set; }
    Item itemvalues = new Item();

    void Start()
    {
        value = 5;
        itemvalues.count = 3;
    }

    public void GetItem()
    {
        Debug.Log("πÊ≈∫∫π»πµÊ");
        GameManager.instance.itemcheck[4] = true;
        //player = gameObject.GetComponent<Player>();
        //player.itemGet[0] = true;
    }
    public void ItemCharge()
    {
        GameManager.instance.itemcount[4] = itemvalues.count;
    }
}
