using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.ProBuilder.MeshOperations;

public class CCTVMovement : MonoBehaviour       // CCTV의 속성을 가지고 있으며, CCTV의 시야를 토대로 경계 단계를 제어함
{
    public enum cctv_state
    {
        cidle,                     // 일반
        detecting              // 플레이어 감지중
    }

    public cctv_state c_state;

    WaitForSeconds inReverse;                       // 플레이어가 시야 내에 "계속" 있을 때 경계 단계가 올라가는 제한시간
    WaitForSeconds coolReverse;                   // 플레이어가 나갔다 들어왔을 때 유예시간
    WaitForSeconds wait;

    Sight csight;

    public bool canReverse;                    // 단계가 올라갈 수 있는 유예시간이 지났는지 (true)

    public float rotationSpeed;               // CCTV의 회전 속도
    public float rotationAmount;            // 한 번 회전할 각도
    public float rotationDuration;           // 한 번 회전당 걸리는 시간

    private bool rotateClockwise = true;    // 회전 방향(시계방향)
    

    Quaternion rotationStartPos;                // CCTV의 회전 시작 지점
    Quaternion rotationEndPos;                 // CCTV의 회전이 종료되는 지점

    Quaternion formatStartPos;                  // 초기 회전값으로 돌아가기 시작하는 지점
    Quaternion defaultPos;                         // 초기 회전값

    void Start()
    {
        defaultPos = transform.rotation;                           // defalultPos에 초기 rotation 값 저장
        rotationStartPos = transform.rotation;                  // rotationStartPos에 초기 rotation 값 저장

        StartCoroutine(AngleMove());                                // CCTV 좌우 회전 코루틴 호출

        inReverse = new WaitForSeconds(5f);                   // 시야 내에 "계속" 있을 때 경계 단계가 올라가는 시간 5초
        coolReverse = new WaitForSeconds(15f);             // CCTV 유예 시간 15초
        canReverse = true;                                                 // 유예 시간이 지나면 단계 전환 가능 체크
        csight = GetComponent<Sight>();
        StartCoroutine(CCTVStateCheck());                        // 시야 내에 적의 유무로 CCTV의 상태를 변경해주는 코루틴 호출
        c_state = cctv_state.cidle;                                       // CCTV의 초기 상태는 일반상태
    }

    void Update()
    {
        if (c_state == cctv_state.detecting)                          // 적 감지중일 경우
        {
            StartCoroutine(DetectCCTVLevel());                     // 경계레벨을 올려주는 코루틴 호출
        }
        //if (csight.findT)
        //{
        //    CCTVHomingPlayer();
        //}
        //if(!csight.findT) 
        //{
        //    CCTVFormatRotate();
        //}
    }

    void CCTVHomingPlayer()                         // 플레이어가 시야에 들어왔을 때 시야에 저장된 플레이어와의 방향값을 이용해 시야각 유도
    {
        Quaternion homingRotation = Quaternion.LookRotation(csight.dir_T, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, homingRotation, 0.05f);
    }

    void CCTVFormatRotate()                         // 플레이어가 시야에 벗어났을 때 초기 저장된 오브젝트의 회전값을 불러와 rotation 값 초기화
    {
        formatStartPos = transform.rotation;
        transform.rotation = Quaternion.Slerp(formatStartPos, defaultPos, 1f);
    }

    IEnumerator AngleMove()
    {
            while (true)
             {
                rotationStartPos = transform.rotation;                                                                           // 또 초기화하는 이유는 회전이 끝날 때 마다 회전 시작지점이 바뀌므로(=회전의 방향이 바뀌므로)
                float targetAngle = rotateClockwise ? rotationAmount : -rotationAmount;                  // Clockwise가 True면  Amount(=시계방향) False면  -Amount(=반시계방향)
                rotationEndPos = Quaternion.Euler(0, targetAngle, 0) * rotationStartPos;                    // 회전 종료 지점을 rotationAmount(-rotationAmount) 값으로 초기화 시켜줌
                                                                                                                                                       // Quaternion.Euler() 오일러각을 쿼터니언으로 변환시켜줌

                float rotatingTime = 0f;                                                // 회전중인 시간 초기화
                while (rotatingTime < rotationDuration)                      // 회전중인 시간이 총 회전 시간이 되지 않았을 때(=아직 회전 중일 때)
                {
                    transform.rotation = Quaternion.Lerp(rotationStartPos, rotationEndPos, rotatingTime / rotationDuration);       // 회전 시작지점에서 종료지점까지 (회전중인 시간) / (총 회전 시간)으로 보간하며 회전
                    rotatingTime += Time.deltaTime;                             // 회전중인 시간 최신화
                    yield return null;                                                        // 회전이 끝날 때 까지 반복해주기 위해서 return null
                }
                rotateClockwise = !rotateClockwise;                             // 회전이 끝났다면 Clockwise의 bool 값을 반대로 변환
                yield return new WaitForSeconds(2f);                           // delay의 초가 지나면 코루틴 재호출
             }
    }

    IEnumerator DetectCCTVLevel()           // 플레이어를 탐지중일때 경계레벨 증가 (유예시간이 끝났을 때, 시야 내에 5초 이상 들어와 있을 때)
    {
        if(canReverse)                                   // CCTV의 단계 전환이 가능할 때 (유예시간이 끝났을 때)
        {
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            {
                canReverse = false;                                                                                                                                         // 경계 단계 전환 불가능
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level2;                                                                              // 경계 단계 1 -> 2
                StartCoroutine(CCTVReverseCheck());                                                                                                            // 유예 시간 코루틴 호출
            }
            yield return inReverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            {
                canReverse = false;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;                                                                              // 경계 단계 2 -> 3
                StartCoroutine(CCTVReverseCheck());                                                                                                            // 유예 시간 코루틴 호출
            }
            yield return inReverse;
            if (c_state == cctv_state.detecting && EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            {
                canReverse = false;
                EnemyLevel.enemylv.LvStep = EnemyLevel.ELevel.level3;                                                                              // 경계 단계 3 유지
                GameManager.instance.lv3PlayerPos = csight.detectTarget.position;
                StartCoroutine(CCTVReverseCheck());                                                                                                            // 유예 시간 코루틴 호출
            }
        }
    }

    IEnumerator CCTVStateCheck()                                // CCTV의 상태를 바꿔주는 코루틴
    {
        if (csight.findT)                                                       // 적이 시야 내에 들어왔을 때
        {
            c_state = cctv_state.detecting;                           // CCTV의 상태를 detecting(적 감지중)으로 전환
        }
        else if(!csight.findT)                                                // 적이 시야에서 사라졌을 때
        {
            c_state = cctv_state.cidle;                                   // CCTV의 상태를 cidle(평상시)로 전환
        }
        yield return wait;
        StartCoroutine(CCTVStateCheck());                        // CCTV 상태 체크 반복
    }

    IEnumerator CCTVReverseCheck()              // 유예시간 15초 코루틴
    {
        yield return coolReverse;                        // 유예시간 15초가 지나면
        canReverse = true;                                  // 경계 단계 전환 가능
    }
}
