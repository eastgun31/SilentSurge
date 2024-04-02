using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamController : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    Vector3 yPos;

    private void Awake()
    {
        yPos = new Vector3 (0, 10, 0);
        transform.position = playerPos.position + yPos;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerPos.position + yPos;
    }
}
