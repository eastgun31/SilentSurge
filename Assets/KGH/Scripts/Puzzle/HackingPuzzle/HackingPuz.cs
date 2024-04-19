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
        // 패스워드 설정
        passwords = new string[inputTexts.Length];
        passwords[0] = "5V";
        passwords[1] = "RB";
        passwords[2] = "Z5";
        passwords[3] = "4K";

        // 첫 번째 텍스트 필드에 초기값 설정
        inputTexts[currentInputIndex].text = "";
    }
    private void Update()
    {
        TimeRemainig();
    }

    public void TimeRemainig() // 퍼즐 제한시간
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
        
        if (currentInputIndex <= inputTexts.Length - 1) // 현재 입력 중인 텍스트 필드가 최대 인덱스 이하인지 확인
        {
            inputTexts[currentInputIndex].text += number;

            if (inputTexts[currentInputIndex].text == passwords[currentInputIndex]) // 현재 텍스트 필드의 입력값이 정답과 일치하는지 확인
            {
                
                currentInputIndex++;  // 정답일 경우 다음 텍스트 필드로 이동
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
                currentInputIndex = 0;     // 오답일 경우 첫 번째 텍스트 필드로 돌아감
                foreach (var text in inputTexts)
                {
                    text.text = "";       // 모든 텍스트 필드의 내용을 지움
                }
                inputTexts[currentInputIndex].text = "";  // 첫 번째 텍스트 필드의 내용을 지움
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
