using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sight : MonoBehaviour
{
    public float radius = 45f;
    [Range(0,360)]
    public float angle;

    public LayerMask playerM;

    public Collider isColP;

    public CCTVMovement cctv;
    public Enemy enemy;

    void Start()
    {
        StartCoroutine(DetectDelay(0.2f));
    }

    IEnumerator DetectDelay(float delay)    // 탐지 딜레이 (0.2초)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            DetectTargets();
        }
    }

    void DetectTargets()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, playerM);
        for (int i = 0; i < targets.Length; i++)
        {
            Transform detectTarget = targets[i].transform;
            Vector3 dirT = (detectTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirT) < angle / 2)
            {
                float disT = Vector3.Distance(transform.position, detectTarget.position);
                foreach(Collider col in targets)
                { 
                    if (this.name == "CCTV")    // CCTV가 플레이어 감지 후 즉시 탐지단계 상승
                    {

                    }
                    if (this.tag == "Enemy")    // 적이 플레이어 감지 후 조건에 부합할 경우 탐지단계 상승
                    {

                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal) // 범위 그려주기 위해 필요한 함수 (오일러 각을 3차원 벡터 값으로 변환)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
