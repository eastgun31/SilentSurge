using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    EnemyInfo enemyInfo = new EnemyInfo();

    public int scenenum;
    public bool enemyDown;
    public bool[] itemcheck;
    public int[] itemcount;
    public int puzzleLevel = 1;
    public bool nowpuzzle = false;
    public bool canUse = true;
    public bool playerchasing = false;
    public bool[] existItem;
    public bool[] existEnemy1 = new bool[8];
    public bool[] existEnemy2 = new bool[12];
    public bool isHide=false;
    public bool isDie=false;
    public Vector3 lv3PlayerPos;

    public GameObject[] Items;

    public void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        SecenCheck();
        enemyDown = false;
        lv3PlayerPos = new Vector3(0,0,0);
        itemcheck = new bool[5] { false, false, false, false, false };
        itemcount = new int[5] { 0, 0, 0, 0, 0 };   //±ÇÃÑ, ÄÚÀÎ, ¼¶±¤Åº, ½É¹ÚÃøÁ¤±â, ¹æÅºº¹
        existItem = new bool[5] { false, true, true, true, false };
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
        if(GameManager.instance.scenenum == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                existEnemy1[i] = true;
            }
        }
        if(GameManager.instance.scenenum == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                existEnemy2[i] = true;
            }
        }
    }
    public void EnemyActive2()
    {
        if(GameManager.instance.scenenum == 1)
        {
            for (int i = 4; i < existEnemy1.Length; i++)
            {
                existEnemy1[i] = true;
            }
        }
        if (GameManager.instance.scenenum == 2)
        {
            for (int i = 6; i < existEnemy2.Length; i++)
           {
                existEnemy2[i] = true;
            }
        }

    }

    void SecenCheck()
    {
        
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("¾À1");
                scenenum = 1;
                existEnemy1 = new bool[9];
                break;
            case 2:
                Debug.Log("¾À2");
                scenenum = 2;
                existEnemy2 = new bool[12];
                break;
            case 3:
                scenenum = 3;
                break;
            case 4:
                scenenum = 4;
                break;
        }

    }

}
