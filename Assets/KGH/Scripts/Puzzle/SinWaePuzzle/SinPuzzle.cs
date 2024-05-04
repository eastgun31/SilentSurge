using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinPuzzle : MonoBehaviour
{

    public LineRenderer lineRenderer;
    [SerializeField][Range(0, 500)] private int points = 260;
    public float amplitude;    // �븻 ����
    public float frequency;    // �븻 ��

    public float correctAmlitude;
    public float correctFrequance;


    public bool isAmplitude = false;
    public bool isFrequance = false;

    public Vector2 xlimit = new Vector2(-600, 600);
    public float speed = 1;

    public Text success;

    public GameObject cctv1;
    public GameObject fcctv1;
    public GameObject cctv2;
    public GameObject fcctv2;

    bool lev = false;

    Player.PlayerState player;

    private void Start()
    {
        amplitude = GameManager.instance.amplitude;
        frequency = GameManager.instance.frequency;
        correctAmlitude = GameManager.instance.correctAmlitude;
        correctFrequance = GameManager.instance.correctFrequance;
        //Difficulty();
    }
    private void OnDisable()
    {
        amplitude = GameManager.instance.amplitude;
        frequency = GameManager.instance.frequency;
    }
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {   
        TimeRemainig();
        WaveControl();
        Wave();
        Win();
        
    }
    public void TimeRemainig() // ���� ���ѽð�
    {
        if (!UiManager.instance.isWin)
        {
            if ((int)UiManager.instance.timeRemainig == 0)
            {
                UiManager.instance.isGameOver = true;
                UiManager.instance.gameover.SetActive(true);
                CloseSin();
            }
            else
            {
                UiManager.instance.timeRemainig -= Time.deltaTime;
                success.text = "Time:  " + (int)UiManager.instance.timeRemainig;
            }  
        }
        else
        {
            success.text = "SUCCESS";
        }
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
        if (UiManager.instance.isWin ==false)
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
        correctFrequance = Mathf.Round(correctFrequance * 1000) * 0.001f;

        if (amplitude == correctAmlitude && frequency == correctFrequance)
        {
            UiManager.instance.isWin = true;
            player = Player.PlayerState.idle;
            if (!lev)
            {
                cctv1.SetActive(false);
                fcctv1.SetActive(true);
                cctv2.SetActive(false);
                fcctv2.SetActive(true);
                GameManager.instance.puzzleLevel += 1; 
                GameManager.instance.nowpuzzle = false;
                GameManager.instance.EnemyActive2();
                EnemyLevel.enemylv.SetEnemy();
                DataManager.instance.SaveData();
                lev = true;
            }
            Invoke("CloseSin", 1f);
            
        }
    }
    private void CloseSin()
    {
        UiManager.instance.isWin = false;
        SubtitleCheck();
        UiManager.instance.CloseSinFst();
    }

    public void SubtitleCheck()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(7);
                break;
            case 2:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(7);
                break;
        }
    }
}
