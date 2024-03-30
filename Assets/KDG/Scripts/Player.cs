using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerspeed = 1f;

    [SerializeField]
    private bool handgunget = false;
    private bool handgunacivate = false;
    [SerializeField]
    private bool coinget = false;
    private bool coinacivate = false;
    [SerializeField]
    private bool flashbangget = false;
    private bool flashbangacivate = false;
    [SerializeField]
    private bool heartseeget = false;
    private bool heartseeacivate = false;

    [SerializeField]
    IItem handgun;    
    [SerializeField]
    IItem coin;    
    [SerializeField]
    IItem flashbang;    
    [SerializeField]
    IItem heartsee;

    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 velocity;
    bool itemActivate = false;

    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;
        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);


        if(handgunget)
        {
            ItemActivate1();
            if(handgunacivate && !coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                handgun.UseItem();
            }
        }
        if(coinget)
        {
            ItemActivate2();
            if(!handgunacivate && coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                coin.UseItem();
            }
        }
        if(flashbangget)
        {
            ItemActivate3();
            if(!handgunacivate && !coinacivate && flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                flashbang.UseItem();
            }
        }
        if(heartseeget)
        {
            ItemActivate4();
            if(!handgunacivate && !coinacivate && !flashbangacivate && heartseeacivate && Input.GetMouseButtonDown(0))
            {
                heartsee.UseItem();
            }
        }


        //Vector3 mousePos = Input.mousePosition;
        //mousePos = cam.ScreenToWorldPoint(mousePos);
        //float zDeg = mousePos.z - rigid.position.z;
        //float xDeg = mousePos.x - rigid.position.x;
        //rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg)- 90);
        //rigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
    }

    void ItemActivate1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !handgunacivate)
        {
            handgunacivate = true;
            Debug.Log("���� Ȱ��ȭ");
            coinacivate = false;
            flashbangacivate = false;
            heartseeacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && handgunacivate)
        {
            handgunacivate = false;
            Debug.Log("���� ��Ȱ��ȭ");
        }
    }
    void ItemActivate2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !coinacivate)
        {
            coinacivate = true;
            Debug.Log("���� Ȱ��ȭ");
            flashbangacivate = false;
            heartseeacivate = false;
            handgunacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && coinacivate)
        {
            coinacivate = false;
            Debug.Log("���� ��Ȱ��ȭ");
        }
    }
    void ItemActivate3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !flashbangacivate)
        {
            flashbangacivate = true;
            Debug.Log("����ź Ȱ��ȭ");
            heartseeacivate = false;
            handgunacivate = false;
            coinacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && flashbangacivate)
        {
            flashbangacivate = false;
            Debug.Log("����ź ��Ȱ��ȭ");
        }
    }
    void ItemActivate4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !heartseeacivate)
        {
            heartseeacivate = true;
            Debug.Log("����ڵ������� Ȱ��ȭ");
            handgunacivate = false;
            coinacivate = false;
            flashbangacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && heartseeacivate)
        {
            heartseeacivate = false;
            Debug.Log("����ڵ������� ��Ȱ��ȭ");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        
        switch(item.value)
        {
            case 1:
                Debug.Log("���� ȹ��");
                handgunget = true;
                handgun = item;
                break;
            case 2:
                Debug.Log("���� ȹ��");
                coinget = true;
                coin = item;
                break;
            case 3:
                Debug.Log("����ź ȹ��");
                flashbangget = true;
                flashbang = item;
                break;
            case 4:
                Debug.Log("����ڵ������� ȹ��");
                heartseeget = true;
                heartsee = item;
                break;
        }
        Destroy(other.gameObject);
    }
}
