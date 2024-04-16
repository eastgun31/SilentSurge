using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public Transform bulletPos;
    [SerializeField]
    private Transform[] bulletPoses;
    [SerializeField]
    public float bulletSpeed = 5f;

    E_CoolTime cooltime;

    void Start()
    {
        cooltime = new E_CoolTime();
    }


}
