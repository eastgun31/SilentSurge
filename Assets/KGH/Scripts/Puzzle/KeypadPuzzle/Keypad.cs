using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{

    public GameObject keypad;

    [SerializeField] private Text answerInput;
    private int maxNum = 4;

    [SerializeField]
    private string pw = "8324";

    public GameObject door;
    BDoor bDoor;

    private void Start()
    {
        bDoor = door.GetComponent<BDoor>();
    }

    private void Update()
    {
        //UiManager.instance.TimeRemainig();
    }

    public void Number(int number) 
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

    public void Enter()
    {
        if(answerInput.text.Equals(pw, System.StringComparison.OrdinalIgnoreCase)) // �Է°��� ������ ������
        {
            answerInput.text = "CORRECT";     // ����
            UiManager.instance.isWin = true;
            PuzLevUp();
            Invoke("Closed", 2f);
        }
        else
        {
            answerInput.text = "INCORRECT";  // ����
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
        bDoor.BDoorOpen();
        GameManager.instance.nowpuzzle = false;
        UiManager.instance.isWin = false;
        UiManager.instance.CloseKeypadFst();
    }
}
