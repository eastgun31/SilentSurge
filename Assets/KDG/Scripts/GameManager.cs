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

    public void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        itemcheck = new bool[5] { false, false, false, false, false };
        itemcount = new int[5] { 0, 0, 0, 0, 0 };   //±ÇÃÑ, ÄÚÀÎ, ¼¶±¤Åº, ½É¹ÚÃøÁ¤±â, ¹æÅºº¹
    }

}
