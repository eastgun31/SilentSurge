using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipes : MonoBehaviour, IPointerClickHandler
{

    //float[] rotation = { 0, 90, 180, 270 };
    public float rotationAngle = 90f;

    public float[] correctRotation; // �ùٸ� ������ �Ǿ����� Ȯ��
    [SerializeField]
    bool isPlaces = false;  //���ĸ� ������ �������� Ȯ��

    int PossibleRots = 1;

    PipeManager pipeManager;

    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();
    }
    private void Update()
    {
        WinCheck();
    }

    void Start()
    {
        PossibleRots = correctRotation.Length; 
       // int rand = Random.Range(0,rotation.Length);
       // transform.eulerAngles = new Vector3(0, 0, rotation[rand]);

        if (PossibleRots > 1) //�ʱ�ȸ�������� Ȯ��
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaces = true;
                pipeManager.CorrectMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaces = true;
                pipeManager.CorrectMove();
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (!WinCheck())
        {
            rt.Rotate(new Vector3(0, 0, rotationAngle)); //Ŭ���Ҷ����� ȸ��

            if (PossibleRots > 1) //ȸ����Ų�� �ùٸ� �������� Ȯ��
            {
                if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1] && isPlaces == false)
                {
                    isPlaces = true;
                    pipeManager.CorrectMove();
                }
                else if (isPlaces == true)
                {
                    isPlaces = false;
                    pipeManager.WrongMove();
                }
            }
            else
            {
                if (transform.eulerAngles.z == correctRotation[0] && isPlaces == false)
                {
                    isPlaces = true;
                    pipeManager.CorrectMove();
                }
                else if (isPlaces == true)
                {
                    isPlaces = false;
                    pipeManager.WrongMove();
                }
            }
        }
    }
    private bool WinCheck()
    {
        return UiManager.instance.isWin;
    }
}
