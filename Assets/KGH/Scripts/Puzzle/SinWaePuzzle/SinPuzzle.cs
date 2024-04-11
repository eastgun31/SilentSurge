using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SinPuzzle : MonoBehaviour
{

    public LineRenderer lineRenderer;
    [SerializeField] [Range(0, 500)] private int points = 260;
    public float amplitude = 70;   // ����
    public float frequency = 0.009f;   // ��
    public Vector2 xlimit = new Vector2(-600, 600);
    public float speed = 1;


    public GameObject cctv;

    bool lev = false;

    Player.PlayerState player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        WaveControl();
        Win();
        Wave();
        UiManager.instance.TimeRemainig();
    }

    private void Wave()
    {
        float xStart = xlimit.x; //x�� ������
        float pi = Mathf.PI;
        float xFinish = xlimit.y; //x�� ����

        lineRenderer.positionCount = points;
        for (int i = 0; i < points; i++)
        {
            float progress = (float)i / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress); //���������� ���������� ����Ʈ ��ġ
            //���α׷��� ���� 0.0�� 1.0������ ��, �� ���� ���ϸ� 1������ ���� �׷�������, frequency�� ���Ͽ� ������ ����
            float y = amplitude * Mathf.Sin((pi * frequency * x) + (Time.timeSinceLevelLoad * speed));

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    private void WaveControl()
    {
        if (!IsWining())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                amplitude += 10;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                amplitude -= 10;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                frequency -= 0.001f;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                frequency += 0.001f;
            frequency = Mathf.Round(frequency * 1000) * 0.001f;
        }

    }

    void Win()
    {
        if (amplitude == 100 && 0.004f == frequency)
        {
            UiManager.instance.isWin = true;
            player = Player.PlayerState.idle;
            if (!lev)
            {
                GameManager.instance.puzzleLevel += 1;
                GameManager.instance.nowpuzzle = false;
                cctv.SetActive(false);
                lev = true;
            }
            Invoke("CloseSin", 2f);
        }
    }
    private bool IsWining()
    {
        return UiManager.instance.isWin;
    }
    private void CloseSin()
    {
        UiManager.instance.CloseSinFst();
    }
}
