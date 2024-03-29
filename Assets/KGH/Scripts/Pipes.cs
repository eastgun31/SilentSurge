using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipes : MonoBehaviour, IPointerClickHandler
{

    float[] rotation = { 0, 90, 180, 270 };
    public float rotationAngle = 90f;

    public float correctRotation;
    [SerializeField]
    bool isPlaces = false;

    void Start()
    {
        int rand = Random.Range(0,rotation.Length);
        transform.eulerAngles = new Vector3(0, 0, rotation[rand]);

        if (transform.eulerAngles.z == correctRotation)
            isPlaces = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {        
        RectTransform rt = GetComponent<RectTransform>();

        rt.Rotate(new Vector3(0, 0, rotationAngle));

        if(transform.eulerAngles.z == correctRotation && isPlaces == false)
            isPlaces = true;
        else if(isPlaces == true)
            isPlaces = false;
    }
}
