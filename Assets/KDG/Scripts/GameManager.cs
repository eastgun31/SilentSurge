using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    EnemyInfo enemyInfo = new EnemyInfo();

    public GameObject gameOver;

    public int scenenum;
    public bool enemyDown;
    public bool[] itemcheck;
    public int[] itemcount;
    public int puzzleLevel;
    public bool nowpuzzle = false;
    public bool canUse = true;
    public bool playerchasing = false;
    public bool[] existItem;
    public bool[] existEnemy;
    public bool isHide=false;
    public bool isDie=false;
    public int playerviewR;
    public int playerviewA;
    [SerializeField]
    private int enemyQuater;
    
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
        for (int i = 0; i < enemyQuater; i++)
        {
            existEnemy[i] = true;
        }
        //if (GameManager.instance.scenenum == 1)
        //{
        //    for (int i = 0; i < enemyQuater; i++)
        //    {
        //        existEnemy[i] = true;
        //    }
        //}
        //if(GameManager.instance.scenenum == 2)
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        existEnemy[i] = true;
        //    }
        //}
    }
    public void EnemyActive2()
    {
        for (int i = enemyQuater; i < existEnemy.Length; i++)
        {
            existEnemy[i] = true;
        }
        //if (GameManager.instance.scenenum == 1)
        //{
        //    for (int i = enemyQuater; i < existEnemy.Length; i++)
        //    {
        //        existEnemy[i] = true;
        //    }
        //}
        //if (GameManager.instance.scenenum == 2)
        //{
        //    for (int i = 6; i < existEnemy.Length; i++)
        //   {
        //        existEnemy[i] = true;
        //    }
        //}

    }

    void SecenCheck()
    {
        
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("¾À1");
                SceneVariableReset(1, EnemyLevel.enemylv.Enemies.Length, 1, 4, 7, 360);
                break;
            case 2:
                Debug.Log("¾À2");
                SceneVariableReset(2, EnemyLevel.enemylv.Enemies.Length, 2, 6, 5, 120);
                break;
            case 3:
                scenenum = 3;
                break;
            case 4:
                scenenum = 4;
                break;
        }
    }

    void SceneVariableReset(int a, int b, int c, int d, int e, int f)
    {
        scenenum = a;
        existEnemy = new bool[b];
        puzzleLevel = c;
        enemyQuater = d;
        playerviewR = e;
        playerviewA = f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

}
