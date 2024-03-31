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
