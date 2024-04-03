using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject keypad;

    [SerializeField] private Text answerInput;
    private int maxNum = 6;

    [SerializeField]
    private string pw = "123456";


    private void Update()
    {
        
    }
    public void Number(int number)
    {
        if (answerInput.text.Length < maxNum) 
        {
            answerInput.text += number.ToString();
        }
    }

    public void Enter()
    {
        if(answerInput.text.Equals(pw, System.StringComparison.OrdinalIgnoreCase))
        {
            answerInput.text = "CORRECT";
            Invoke("CloseKeypad", 2f);
        }
        else
        {
            answerInput.text = "INCORRECT";
            Invoke("ResetAns", 1f);
        }
    }
    public void BackSpace()
    {
        if (answerInput.text.Length > 0)
        {
            answerInput.text = answerInput.text.Substring(0, answerInput.text.Length - 1);
        }
    }

    public void ResetAns()
    {
        answerInput.text = "";
    }

    public void CloseKeypad()
    {
        keypad.SetActive(false);
    }
}
