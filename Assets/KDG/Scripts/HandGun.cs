using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;
using System.Threading;

public class HandGun : MonoBehaviour, IItem
{
    [SerializeField]
    private float bulletSpeed = 1f;

    public int value {  get; set; }
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    Player player;
    

    void Start()
    {
        value = 1;
        
    }

    public void GetItem()
    {
        Debug.Log("±ÇÃÑÈ¹µæ");
        GameManager.instance.itemcheck[0] = true;
        //player = gameObject.GetComponent<Player>();
        //player.itemGet[0] = true;
    }
    public void ItemCharge()
    {

    }
}
