using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{

    public GameObject keypad;

    [SerializeField] private Text answerInput;
    private int maxNum = 4;

    private string pw;

    public UnityEvent doorOpen;

    GameManager gm;
    UiManager um;


    private void OnDisable()
    {
        answerInput.text = "";
    }

    private void Start()
    {
        um = UiManager.instance;
        gm = GameManager.instance;
        pw = GameManager.instance.paswawrd;
    }

    public void Number(int number) 
    {
        if (um.isWin == false ||  
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
            um.isWin = true;
            PuzLevUp();
            SceneCheck();
            Invoke("Closed", 1f);
        }
        else
        {
            answerInput.text = "INCORRECT";  // 오답
            Invoke("ResetAns", 1f);
        }
    }
    public void BackSpace()
    {
        if (um.isWin == false)
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
        gm.nowpuzzle = false;
        gm.puzzleLevel += 1;
        DataManager.instance.SaveData();
    }

    void SceneCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
            doorOpen.Invoke();
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            gm.rescueHostage = true;
    }

    public void Closed()
    { 
        um.isWin = false;
        um.CloseKeypadFst();
    }
}
