using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    public enum ELevel //��跹�� ���¸ӽ�
    {
        level1, level2, level3
    }

    public ELevel LvStep;

    public static EnemyLevel enemylv;

    public void Awake()
    {

        if (enemylv != null)
            Destroy(gameObject);
        else
            enemylv = this;

        LvStep = ELevel.level1;
    }
}
