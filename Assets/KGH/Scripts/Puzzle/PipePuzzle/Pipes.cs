using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipes : MonoBehaviour, IPointerClickHandler
{

    //float[] rotation = { 0, 90, 180, 270 };
    public float rotationAngle = 90f;

    public float[] correctRotation; // 올바른 각도가 되었는지 확인
    [SerializeField]
    bool isPlaces = false;  //올파른 각도를 가지는지 확인

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

        if (PossibleRots > 1) //초기회전각도를 확인
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
            rt.Rotate(new Vector3(0, 0, rotationAngle)); //클릭할때마다 회전

            if (PossibleRots > 1) //회전시킨후 올바른 각도인지 확인
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
