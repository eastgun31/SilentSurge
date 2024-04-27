using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{

    public GameObject keypad;

    [SerializeField] private Text answerInput;
    private int maxNum = 4;

    private string pw;

    public UnityEvent doorOpen;

    private void OnDisable()
    {
        answerInput.text = "";
    }

    private void Start()
    {
        pw = GameManager.instance.paswawrd;
        //if(GameManager.instance.scenenum ==1)
        //    paswawrd = "8324";
        //if (GameManager.instance.scenenum == 2)
        //    paswawrd = "9635";
    }

    public void Number(int number) 
    {
        if (UiManager.instance.isWin == false ||  
            !answerInput.text.Equals(pw, System.StringComparison.OrdinalIgnoreCase)) // 퍼즐 성공 또는 입력 숫자가 정답과 아닐때만 입력 가능
        {
            if (answerInput.text.Length < maxNum) // 정답의 길이보다 작을때만 
            {
                answerInput.text += number.ToString();
            }
        }
    }

    public void Enter()
    {
        if(answerInput.text.Equals(pw, System.StringComparison.OrdinalIgnoreCase)) // 입력값과 정답이 같으면
        {
            answerInput.text = "CORRECT";     // 정답
            UiManager.instance.isWin = true;
            PuzLevUp();
            Invoke("Closed", 2f);
        }
        else
        {
            answerInput.text = "INCORRECT";  // 오답
            Invoke("ResetAns", 1f);
        }
    }
    public void BackSpace()
    {
        if (UiManager.instance.isWin == false)
        {
            if (answerInput.text.Length > 0)
                answerInput.text = answerInput.text.Substring(0, answerInput.text.Length - 1);
        }
    }

    public void ResetAns()
    {
        answerInput.text = "";
    }

    public void PuzLevUp()
    {
        GameManager.instance.puzzleLevel += 1;
        DataManager.instance.SaveData();
    }

    public void Closed()
    {
        doorOpen.Invoke();
        GameManager.instance.nowpuzzle = false;
        UiManager.instance.isWin = false;
        UiManager.instance.CloseKeypadFst();
    }
}
