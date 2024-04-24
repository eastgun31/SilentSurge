using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class SinWave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField][Range(0, 500)] private int points = 50;
    public float amplitude;
    public float frequency;
    public Vector2 xlimit = new Vector2(-8,8);
    public float speed = 1;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }


    void Update()
    {
        if (GameManager.instance.scenenum == 1)
            Normal();
        if (GameManager.instance.scenenum == 2)
            Hard();
    }
    private void Wave()
    {
        float xStart = xlimit.x; //x의 시작점
        float pi = Mathf.PI;
        float xFinish = xlimit.y; //x의 끝점

        lineRenderer.positionCount = points;
        for (int i = 0; i < points; i++) 
        {
            float progress = (float)i/(points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress); //시작점부터 끝지점까지 포인트 배치
            //사인그래프 생성 0.0과 1.0사이의 값, 이 값을 곱하면 1진동의 사인 그래프생성, frequency를 곱하여 진동수 결정
            float y = amplitude*Mathf.Sin((pi*frequency * x)+(Time.timeSinceLevelLoad*speed));
           
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    private void Normal()
    {
        amplitude = 100f;
        frequency = 0.004f;
        Wave();
    }
    private void Hard()
    {
        amplitude = 90;
        frequency = 0.005f;
        Wave(); 
    }

}
