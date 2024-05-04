using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                    Invoke("CloseHackingPuz", 2f);
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
        DataManager.instance.SaveData();
    }
    public void CloseHackingPuz()
    {
        um.isWin = false;
        SubtitleCheck();
        hacking.SetActive(false);
    }

    public void SubtitleCheck()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(8);
                break;
            case 2:
                Debug.Log("��1");
                GuideLineTxt.instance.SetDifferentTxt(8);
                break;
        }
    }
}
