using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool[] itemcheck;
    public int[] itemcount;
    public int puzzleLevel = 1;
    public bool nowpuzzle = false;
    public bool canUse = true;
    public bool playerchasing = false;
    public bool[] existItem;
    public bool[] existEnemy;
    public bool isHide=false;

    public GameObject[] Items;

    public void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        itemcheck = new bool[5] { false, false, false, false, false };
        itemcount = new int[5] { 0, 0, 0, 0, 0 };   //±ÇÃÑ, ÄÚÀÎ, ¼¶±¤Åº, ½É¹ÚÃøÁ¤±â, ¹æÅºº¹
        existItem = new bool[5] { false, true, true, true, false };
        existEnemy = new bool[14];
        for(int i = 0; i < 9; i++)
        {
            existEnemy[i] = true;
        }
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

}
