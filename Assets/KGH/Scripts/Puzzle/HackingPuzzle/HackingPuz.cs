using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingPuz : MonoBehaviour
{
    public GameObject hacking;

    [SerializeField] private Text answerInput;
    private int maxNum = 8;

    [SerializeField]
    private string pw = "5VRBZ54K";


    void Update()
    {
        UiManager.instance.TimeRemainig();
    }

    public void Number(string number)
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
}
