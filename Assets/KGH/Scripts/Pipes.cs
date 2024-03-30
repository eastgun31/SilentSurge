using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipes : MonoBehaviour, IPointerClickHandler
{

    float[] rotation = { 0, 90, 180, 270 };
    public float rotationAngle = 90f;

    public float[] correctRotation;
    [SerializeField]
    bool isPlaces = false;

    int PossibleRots = 1;

    PipeManager pipeManager;

    private void Awake()
    {
        pipeManager = GameObject.Find("PipeManager").GetComponent<PipeManager>();
    }

    void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0,rotation.Length);
        transform.eulerAngles = new Vector3(0, 0, rotation[rand]);

        if (PossibleRots > 1)
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

        rt.Rotate(new Vector3(0, 0, rotationAngle));

        if (PossibleRots > 1)
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
