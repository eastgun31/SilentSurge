using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class FlashBang : MonoBehaviour, IItem
{
    public int value { get; set; }

    Player player;

    void Start()
    {
        value = 3;
    }


    public void GetItem()
    {
        Debug.Log("¼¶±¤ÅºÈ¹µæ");
        GameManager.instance.itemcheck[2] = true;
        //player = gameObject.GetComponent<Player>();
        //player.itemGet[2] = true;
    }
    public void ItemCharge()
    {

    }
}
