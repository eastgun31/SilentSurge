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

    GameManager gm;
    UiManager um;

    private void Start()
    {
        gm = GameManager.instance;
        um = UiManager.instance;
        amplitude = gm.amplitude;
        frequency = gm.frequency;
        correctAmlitude = gm.correctAmlitude;
        correctFrequance = gm.correctFrequance;
        //Difficulty();
    }
    private void OnDisable()
    {
        amplitude = gm.amplitude;
        frequency = gm.frequency;
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
                um.isGameOver = true;
                um.gameover.SetActive(true);
                IncorrectClose();
            }
            else
            {
                um.timeRemainig -= Time.deltaTime;
                success.text = "Time:  " + (int)um.timeRemainig;
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
        if (um.isWin ==false)
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
            um.isWin = true;
            if (!lev)
            {
                cctv1.SetActive(false);
                fcctv1.SetActive(true);
                cctv2.SetActive(false);
                fcctv2.SetActive(true);
                gm.puzzleLevel += 1; 
                
                gm.EnemyActive2();
                EnemyLevel.enemylv.SetEnemy();     
                SubtitleCheck();
                
                lev = true;
            }
            Invoke("CloseSin", 1f);
            
        }
    }
    private void CloseSin()
    {
        um.isWin = false;
        gm.nowpuzzle = false;
        DataManager.instance.SaveData();
        um.CloseSinFst();
    }
    private void IncorrectClose()
    {
        um.isWin = false;
        gm.nowpuzzle = false;
        um.CloseSinFst() ;
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
