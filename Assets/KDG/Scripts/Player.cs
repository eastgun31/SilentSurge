using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerspeed = 10f;

    [SerializeField]
    private bool handgunacivate = false;
    [SerializeField]
    private bool coinacivate = false;
    [SerializeField]
    private bool flashbangacivate = false;
    [SerializeField]
    private bool heartseeacivate = false;

    [SerializeField]
    UseItem useItem;
    IItem item;

    public GameObject handGunModel;
    public GameObject bulletPrefab;

    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 mousePos;
    Vector3 velocity;
    bool itemActivate = false;

    void Start()
    {
        useItem = GetComponent<UseItem>();
        rigid = transform.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;
        rigid.MovePosition(rigid.position + velocity * Time.fixedDeltaTime);


        if (GameManager.instance.itemcheck[0])
        {
            ItemActivate1();
            if(handgunacivate && !coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                useItem.GunFire(mousePos);
            }
        }
        if(GameManager.instance.itemcheck[1])
        {
            ItemActivate2();
            if(!handgunacivate && coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                useItem.ThrowCoin();
            }
        }
        if(GameManager.instance.itemcheck[2])
        {
            ItemActivate3();
            if(!handgunacivate && !coinacivate && flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
            }
        }
        if(GameManager.instance.itemcheck[3])
        {
            ItemActivate4();
            if(!handgunacivate && !coinacivate && !flashbangacivate && heartseeacivate && Input.GetMouseButtonDown(0))
            {
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
            handGunModel.SetActive(true);
            Debug.Log("권총 활성화");
            coinacivate = false;
            flashbangacivate = false;
            heartseeacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && handgunacivate)
        {
            handGunModel.SetActive(false);
            handgunacivate = false;
            Debug.Log("권총 비활성화");
        }
    }
    void ItemActivate2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !coinacivate)
        {
            coinacivate = true;
            Debug.Log("코인 활성화");
            handGunModel.SetActive(false);
            flashbangacivate = false;
            heartseeacivate = false;
            handgunacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && coinacivate)
        {
            coinacivate = false;
            Debug.Log("코인 비활성화");
        }
    }
    void ItemActivate3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !flashbangacivate)
        {
            flashbangacivate = true;
            Debug.Log("섬광탄 활성화");
            handGunModel.SetActive(false);
            heartseeacivate = false;
            handgunacivate = false;
            coinacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && flashbangacivate)
        {
            flashbangacivate = false;
            Debug.Log("섬광탄 비활성화");
        }
    }
    void ItemActivate4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !heartseeacivate)
        {
            heartseeacivate = true;
            Debug.Log("심장박동측정기 활성화");
            handGunModel.SetActive(false);
            handgunacivate = false;
            coinacivate = false;
            flashbangacivate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && heartseeacivate)
        {
            heartseeacivate = false;
            Debug.Log("심장박동측정기 비활성화");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            item = other.GetComponent<IItem>();
        }
        
        switch(item.value)
        {
            case 1:
                item.GetItem();
                break;
            case 2:
                item.GetItem();
                break;
            case 3:
                item.GetItem();
                break;
            case 4:
                item.GetItem();
                break;
        }
        Destroy(other.gameObject);
    }
}
