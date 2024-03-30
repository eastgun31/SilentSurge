using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerspeed = 1f;

    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 velocity;
    
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        cam = Camera.main;
        Item items = new Item();
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;
        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = cam.ScreenToWorldPoint(mousePos);
        //float zDeg = mousePos.z - rigid.position.z;
        //float xDeg = mousePos.x - rigid.position.x;
        //rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg)- 90);
        //rigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
    }
}
