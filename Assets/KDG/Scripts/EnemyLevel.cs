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

    WaitForSeconds downTime;
    private bool lvDowning;

    public void Awake()
    {
        if (enemylv != null)
            Destroy(gameObject);
        else
            enemylv = this;

        LvStep = ELevel.level1;
        downTime = new WaitForSeconds(15f);
        lvDowning = false;
    }

    private void Update()
    {
        if (GameManager.instance.playerchasing == true)
            StopCoroutine(LvDown());
        else if(!lvDowning && LvStep != ELevel.level1)
            StartCoroutine(LvDown()); 
    }

    IEnumerator LvDown()
    {
        lvDowning = true;

        yield return downTime;

        if (!GameManager.instance.playerchasing && LvStep == ELevel.level2)
            LvStep = ELevel.level1;
        else if(!GameManager.instance.playerchasing && LvStep == ELevel.level3)
            LvStep = ELevel.level2;

        lvDowning = false;
    }
}
