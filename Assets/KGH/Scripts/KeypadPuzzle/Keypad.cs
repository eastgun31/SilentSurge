using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] private Text answerInput;
    private int maxNum = 6;

    private string pw = "123456";


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
        }
        else
        {
            answerInput.text = "INCORRECT"; 
        }
    }
    public void BackSpace()
    {
        if (answerInput.text.Length > 0)
        {
            answerInput.text = answerInput.text.Substring(0, answerInput.text.Length - 1);
        }
    }
}
