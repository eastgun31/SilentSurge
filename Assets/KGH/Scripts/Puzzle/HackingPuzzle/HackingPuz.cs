using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingPuz : MonoBehaviour
{
    public GameObject hacking;

    public Text success;

    public Text[] inputTexts; 
    private int currentInputIndex = 0; 

    private string[] passwords; 

    

    void Start()
    {
        // �н����� ����
        passwords = new string[inputTexts.Length];
        passwords[0] = "5V";
        passwords[1] = "RB";
        passwords[2] = "Z5";
        passwords[3] = "4K";

        // ù ��° �ؽ�Ʈ �ʵ忡 �ʱⰪ ����
        inputTexts[currentInputIndex].text = "";
    }
    private void Update()
    {
        TimeRemainig();
    }

    public void TimeRemainig() // ���� ���ѽð�
    {
        if (!UiManager.instance.isWin)
        {
            if ((int)UiManager.instance.timeRemainig == 0)
            {
                success.text = "FAIL";
                UiManager.instance.isGameOver = true;
                UiManager.instance.gameover.SetActive(true);
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
            Invoke("ResetTime", 2f);
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
                    PuzlvUp();
                    Invoke("ColseHackingPuz", 2f);
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
        UiManager.instance.isWin = true;
        GameManager.instance.puzzleLevel += 1;
        GameManager.instance.nowpuzzle = false;
        DataManager.instance.SaveData();
    }
    public void ColseHackingPuz()
    {
        UiManager.instance.isWin = false;
        hacking.SetActive(false);
    }
}
