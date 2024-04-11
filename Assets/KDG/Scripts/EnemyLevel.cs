using System.Collections;
using System.Collections.Generic;
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

    WaitForSeconds downTime;
    private bool lvDowning;
    private bool addcomplete;

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
            lv3enemy.SetActive(true);
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
            lv3enemy.SetActive(false);
        }
            

        lvDowning = false;
    }
}
