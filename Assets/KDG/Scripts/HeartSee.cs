using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class HeartSee : MonoBehaviour, IItem
{
    public int value { get; set; }

    Player player;

    void Start()
    {
        value = 4;
    }

    public void GetItem()
    {
        Debug.Log("�ɹ�������ȹ��");
        GameManager.instance.itemcheck[3] = true;
        //player = gameObject.GetComponent<Player>();
        //player.itemGet[3] = true;
    }
    public void ItemCharge()
    {

    }
}
