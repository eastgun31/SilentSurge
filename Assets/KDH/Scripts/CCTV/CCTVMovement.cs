using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CCTVMovement : MonoBehaviour       // CCTV의 탐지 반경을 좌우로 반복시켜주는 스크립트
{
    private Transform target;

    public float rotationSpeed = 20f;       // 카메라의 회전 속도
    public float rotationAmount = 20f;      // 한 번 회전할 각도
    public float rotationDuration = 2f;     // 한 번 회전당 걸리는 시간

    private bool rotateClockwise = true;    // 회전 방향(시계방향)

    Quaternion startRotation;                  // 카메라의 회전 시작 지점
    Quaternion endRotation;                    // 카메라의 회전이 종료되는 지점

    public float angleRange = 30f;
    public float radius = 3f;


    bool isCollision = false;

    Color red1 = new Color(1f, 0f, 0f, 0.2f);
    Color red2 = new Color(0.75f, 0.17f, 0.12f, 0.2f);

    void Start()
    {
        startRotation = transform.rotation;     // 현재 rotation 값을 startRotation에 저장함
        StartCoroutine(AngleMove(2f));          // 코루틴 실행 함수 (딜레이 2초)
    }

    IEnumerator AngleMove(float delay)
    {
        while(true)
        {
            startRotation = transform.rotation;   // 또 초기화하는 이유는 회전이 끝날때 마다 시작지점이 바뀌므로(=회전의 방향이 바뀌므로)

            float targetAngle = rotateClockwise ? rotationAmount : -rotationAmount;     // Clockwise가 True면  Amount(=시계방향) False면  -Amount(=반시계)
            endRotation = Quaternion.Euler(0, targetAngle, 0) * startRotation;                 // y를 해당하는 방향에 맞게 회전          // *startRotation ??????
                                                                                                                                       //Quaternion.Euler(x축 회전, y축 회전, z축 회전)

            float rotatingTime = 0f;                                            // 회전중인 시간 초기화
            while(rotatingTime<rotationDuration)                     // 회전중인 시간이 총 회전 시간이 되지 않았을 때(=아직 회전 중일 때)
            {
                transform.rotation=Quaternion.Lerp(startRotation, endRotation,rotatingTime/rotationDuration);       // 회전 시작 지점에서 종료지점까지 회전을 "회전중인 시간"/"총 회전 시간"으로 보간해줌
                rotatingTime += Time.deltaTime;                         // 회전중인 시간 최신화
                yield return null;                                                    // 회전이 끝날 때 까지 반복해주기 위해서 return null
            }
            rotateClockwise = !rotateClockwise;                         // 회전이 끝났다면 Clockwise의 bool 값을 반대로 변환
            yield return new WaitForSeconds(delay);                  // delay의 초가 지나면 코루틴 재실행
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Handles.color = isCollision ? red1 : red2;          // isCollision이 True면 red1, False면 red2
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);    // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
    //    Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    //}
}
