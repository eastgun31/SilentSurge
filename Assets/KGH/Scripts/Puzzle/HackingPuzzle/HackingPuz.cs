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
            !answerInput.text.Equals(pw, System.StringComparison.OrdinalIgnoreCase)) // ���� ���� �Ǵ� �Է� ���ڰ� ����� �ƴҶ��� �Է� ����
        {
            if (answerInput.text.Length < maxNum) // ������ ���̺��� �������� 
            {
                answerInput.text += number.ToString();
            }
        }
    }
}
