using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
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
    public GameObject[] Enemies;

    WaitForSeconds downTime;
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
        downTime = new WaitForSeconds(15f);
        lvDowning = false;
        addcomplete = false;
        enemyadd = false;
    }

    private void Update()
    {
        if (GameManager.instance.playerchasing == true)
            StopCoroutine(LvDown());
        else if(!lvDowning && LvStep != ELevel.level1)
            StartCoroutine(LvDown()); 

        if(LvStep == ELevel.level3 && !addcomplete)
        {
            Debug.Log("적추가");
            addcomplete = true;
            enemyadd= true;
            if(enemyadd)
            {
                enemyadd = false;
                lv3enemy.SetActive(true);
                for(int i = 0; i < lv3enemy.transform.childCount; i++)
                {
                    lv3enemy.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator LvDown()
    {
        lvDowning = true;

        yield return downTime;

        if (!GameManager.instance.playerchasing && LvStep == ELevel.level2)
        {
            LvStep = ELevel.level1;
        }
        else if(!GameManager.instance.playerchasing && LvStep == ELevel.level3)
        {
            LvStep = ELevel.level2;
            addcomplete = false;
            lv3enemy.SetActive(false);
            enemyadd = false;
        }
            

        lvDowning = false;
    }

    public void SetEnemy()
    {
        for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        {
            if (GameManager.instance.existEnemy[i])
            {
                Enemies[i].SetActive(true);
            }
            else if (!GameManager.instance.existEnemy[i])
            {
                Enemies[i].SetActive(false);
            }
        }
    }
}
