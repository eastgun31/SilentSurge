using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    EnemyInfo enemyInfo = new EnemyInfo();

    public bool enemyDown;
    public bool[] itemcheck;
    public int[] itemcount;
    public int puzzleLevel = 1;
    public bool nowpuzzle = false;
    public bool canUse = true;
    public bool playerchasing = false;
    public bool[] existItem;
    public bool[] existEnemy = new bool[12];
    public bool isHide=false;
    public Vector3 lv3PlayerPos;

    public GameObject[] Items;

    public void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        lv3PlayerPos = new Vector3(0,0,0);
        itemcheck = new bool[5] { false, false, false, false, false };
        itemcount = new int[5] { 0, 0, 0, 0, 0 };   //±ÇÃÑ, ÄÚÀÎ, ¼¶±¤Åº, ½É¹ÚÃøÁ¤±â, ¹æÅºº¹
        existItem = new bool[5] { false, true, true, true, false };
        existEnemy = new bool[12];
        EnemyActive1();
    }

    public void SetItem()
    {
        for (int i = 0; i < existItem.Length; i++)
        {
            if (existItem[i])
            {
                Items[i].SetActive(true); 
            }
            else if(!existItem[i])
            { 
                Items[i].SetActive(false); 
            }
        }
    }

    public void EnemyActive1()
    {
        for (int i = 0; i < 6; i++)
        {
            existEnemy[i] = true;
        }
    }
    public void EnemyActive2()
    {
        for (int i = 6; i < existEnemy.Length; i++)
        {
            existEnemy[i] = true;
        }
    }

}
