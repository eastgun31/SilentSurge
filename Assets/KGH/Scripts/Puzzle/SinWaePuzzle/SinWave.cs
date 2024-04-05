using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinWave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    [SerializeField][Range(0, 500)] private int points = 50;
    public float amplitude = 1;
    public float frequency = 1;
    public Vector2 xlimit = new Vector2(-8,8);
    public float speed = 1;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }


    void Update()
    {
        Wave();
    }
    private void Wave()
    {
        float xStart = xlimit.x; //x�� ������
        float pi = Mathf.PI;
        float xFinish = xlimit.y; //x�� ����

        lineRenderer.positionCount = points;
        for (int i = 0; i < points; i++) 
        {
            float progress = (float)i/(points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress); //���������� ���������� ����Ʈ ��ġ
            //���α׷��� ���� 0.0�� 1.0������ ��, �� ���� ���ϸ� 1������ ���� �׷�������, frequency�� ���Ͽ� ������ ����
            float y = amplitude*Mathf.Sin((pi*frequency * x)+(Time.timeSinceLevelLoad*speed));
           
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
        
    }
}
