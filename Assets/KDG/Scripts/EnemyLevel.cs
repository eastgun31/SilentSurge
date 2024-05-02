using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    public enum ELevel //경계레벨 상태머신
    {
        level1, level2, level3
    }
    public ELevel LvStep;

    public static EnemyLevel enemylv;
    public GameObject lv3enemy;
    public GameObject lastenemy;
    public GameObject[] Enemies;

    WaitForSeconds downTime;
    WaitForSeconds upTime;
    GameManager gm;
    private bool lvDowning;
    private bool addcomplete;
    private bool enemyadd;

    public void Awake()
    {
        if (enemylv != null)
            Destroy(gameObject);
        else
            enemylv = this;

        LvStep = ELevel.level1;
        downTime = new WaitForSeconds(7f);
        upTime = new WaitForSeconds(5f);
        lvDowning = false;
        enemyadd = false;
    }

    private void Start()
    {
        gm = GameManager.instance;
        StartCoroutine(LvUp());
        StartCoroutine(LvDown());
    }

    private void Update()
    {
        //if (gm.playerchasing < 0)
        //    gm.playerchasing = 0;
        if (gm.playerchasing != 0)
            gm.playerchasing -= Time.deltaTime;

        //if (gm.playerchasing)
        //    StopCoroutine(LvDown());
        //else if(!gm.playerchasing && !lvDowning && LvStep != ELevel.level1)
        //    StartCoroutine(LvDown()); 

        //if(LvStep == ELevel.level3 && !addcomplete)
        //{
        //    Debug.Log("적추가");
        //    addcomplete = true;
        //    enemyadd= true;
        //    if(enemyadd)
        //    {
        //        enemyadd = false;
        //        ODaeGi();
        //    }
        //}

    }

    IEnumerator LvUp()
    {
        while (true)
        { 
            if(gm.playerchasing > 10f && !gm.isDie)
            {
                if (LvStep == ELevel.level1)
                    LvStep = ELevel.level2;
                else if(gm.playerchasing >= 20f && LvStep == ELevel.level2)
                {
                    LvStep = ELevel.level3;
                    if(!enemyadd)
                    {
                        enemyadd=true;
                        ODaeGi();
                    }
                }
                else if(LvStep == ELevel.level3)
                    LvStep = ELevel.level3;
                yield return upTime;
            }
            yield return null;
        }
    }
    IEnumerator LvDown()
    {
        while (true)
        {
            if (gm.playerchasing != 0 && LvStep != ELevel.level1)
            {
                if (gm.playerchasing <= 0f && LvStep == ELevel.level2)
                {
                    LvStep = ELevel.level1;
                    gm.playerchasing = 0;
                }
                else if (gm.playerchasing <= 10f && LvStep == ELevel.level3)
                {
                    LvStep = ELevel.level2;
                    lv3enemy.SetActive(false);
                    for (int i = 0; i < lv3enemy.transform.childCount; i++)
                    {
                        lv3enemy.transform.GetChild(i).gameObject.transform.position = lv3enemy.transform.position;
                    }
                    enemyadd = false;
                }
                yield return downTime;
            }
            else if (gm.isDie)
            {
                LvStep = ELevel.level1;
            }
            yield return null;
        }
    }

    public void StateClear()
    {
        gm.playerchasing = 0;
        StopCoroutine(LvUp());
        StopCoroutine(LvDown());
        StartCoroutine(LvUp());
        StartCoroutine(LvDown());
        lv3enemy.SetActive(false);
        for (int i = 0; i < lv3enemy.transform.childCount; i++)
        {
            lv3enemy.transform.GetChild(i).gameObject.transform.position = lv3enemy.transform.position;
        }
        enemyadd = false;
        LvStep = ELevel.level1;
        gm.playerchasing = 0;
    }

    public void SetEnemy()
    {
        for (int i = 0; i < gm.existEnemy.Length; i++)
        {
            if (gm.existEnemy[i])
            {
                Enemies[i].SetActive(true);
            }
            else if (!gm.existEnemy[i])
            {
                Enemies[i].SetActive(false);
            }
        }

        //if (GameManager.instance.scenenum ==1)
        //{
        //    for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        //    {
        //        if (GameManager.instance.existEnemy[i])
        //        {
        //            Enemies[i].SetActive(true);
        //        }
        //        else if (!GameManager.instance.existEnemy[i])
        //        {
        //            Enemies[i].SetActive(false);
        //        }
        //    }
        //}
        //else if(GameManager.instance.scenenum ==2)
        //{
        //    for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        //    {
        //        if (GameManager.instance.existEnemy[i])
        //        {
        //            Enemies[i].SetActive(true);
        //        }
        //        else if (!GameManager.instance.existEnemy[i])
        //        {
        //            Enemies[i].SetActive(false);
        //        }
        //    }
        //}
    }

    public void ODaeGi()
    {
        lv3enemy.SetActive(true);
        for (int i = 0; i < lv3enemy.transform.childCount; i++)
        {
            lv3enemy.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void ODaeGi2()
    {
        lastenemy.SetActive(true);
        for (int i = 0; i < lv3enemy.transform.childCount; i++)
        {
            lastenemy.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
