using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HackingPuz : MonoBehaviour
{
    public GameObject hacking;

    public Text success;

    public Text[] inputTexts; 
    private int currentInputIndex = 0; 

    private string[] passwords;  // �Է� �ڵ�
    public Text[] ansCode;       // ���� �ڵ�

    public UnityEvent doorOpen;

    GameManager gm;
    UiManager um;

    private void OnDisable()
    {
        inputTexts[0].text = "";
        inputTexts[1].text = "";
        inputTexts[2].text = "";
        inputTexts[3].text = "";
    }

    void Start()
    {
        gm = GameManager.instance;
        um = UiManager.instance;

        // �н����� ����
        passwords = new string[inputTexts.Length];
        if(gm.scenenum ==1 )
        {
            ansCode[0].text = "5V";
            ansCode[1].text = "RB";
            ansCode[2].text = "Z5";
            ansCode[3].text = "4K";
            passwords[0] = "5V";
            passwords[1] = "RB";
            passwords[2] = "Z5";
            passwords[3] = "4K";
        }
        if(gm.scenenum ==2 )
        {
            ansCode[0].text = "9H";
            ansCode[1].text = "S1";
            ansCode[2].text = "FF";
            ansCode[3].text = "3D";
            passwords[0] = "9H";
            passwords[1] = "S1";
            passwords[2] = "FF";
            passwords[3] = "3D";
        }
        if(gm.scenenum ==3)
        {
            ansCode[0].text = "S1";
            ansCode[1].text = "5V";
            ansCode[2].text = "Z5";
            ansCode[3].text = "6C";
            passwords[0] = "S1";
            passwords[1] = "5V";
            passwords[2] = "Z5";
            passwords[3] = "6C";
        }
        if( gm.scenenum ==4 ) 
        {
            ansCode[0].text = "4K";
            ansCode[1].text = "9H";
            ansCode[2].text = "S1";
            ansCode[3].text = "4K";
            passwords[0] = "4K";
            passwords[1] = "9H";
            passwords[2] = "S1";
            passwords[3] = "4K";
        }

        // ù ��° �ؽ�Ʈ �ʵ忡 �ʱⰪ ����
        inputTexts[currentInputIndex].text = "";
    }
    private void Update()
    {
        TimeRemainig();
    }

    public void TimeRemainig() // ���� ���ѽð�
    {
        if (!um.isWin)
        {
            if ((int)um.timeRemainig == 0)
            {
                um.isGameOver = true;
                um.gameover.SetActive(true);
                CloseHackingPuz();
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


    public void Number(string number)
    {
        
        if (currentInputIndex <= inputTexts.Length - 1) // ���� �Է� ���� �ؽ�Ʈ �ʵ尡 �ִ� �ε��� �������� Ȯ��
        {
            inputTexts[currentInputIndex].text += number;

            if (inputTexts[currentInputIndex].text == passwords[currentInputIndex]) // ���� �ؽ�Ʈ �ʵ��� �Է°��� ����� ��ġ�ϴ��� Ȯ��
            {
                
                currentInputIndex++;  // ������ ��� ���� �ؽ�Ʈ �ʵ�� �̵�
                if (currentInputIndex < inputTexts.Length)
                    inputTexts[currentInputIndex].text = "";
                else
                {   
                    Invoke("CloseHackingPuz", 1f);
                    PuzlvUp();
                }
            }
            else if (inputTexts[currentInputIndex].text.Length >= passwords[currentInputIndex].Length)
            {
                currentInputIndex = 0;     // ������ ��� ù ��° �ؽ�Ʈ �ʵ�� ���ư�
                foreach (var text in inputTexts)
                {
                    text.text = "";       // ��� �ؽ�Ʈ �ʵ��� ������ ����
                }
                inputTexts[currentInputIndex].text = "";  // ù ��° �ؽ�Ʈ �ʵ��� ������ ����
            }
        }
    }

    public void PuzlvUp()
    {   
        gm.nowpuzzle = false;
        um.isWin = true;

        gm.puzzleLevel += 1;
        SubtitleCheck();
        um.isWin = false;
        DataManager.instance.SaveData();
    }
    public void CloseHackingPuz()
    {
        hacking.SetActive(false);
    }

    public void SubtitleCheck()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                GuideLineTxt.instance.SetDifferentTxt(4);
                break;
            case 2:
                GuideLineTxt.instance.SetDifferentTxt(6);
                break;
            case 3:
                doorOpen.Invoke();
                GuideLineTxt.instance.SetDifferentTxt(16);
                break;
            case 4:
                doorOpen.Invoke();
                GuideLineTxt.instance.SetDifferentTxt(16);
                break;
        }
    }
}
