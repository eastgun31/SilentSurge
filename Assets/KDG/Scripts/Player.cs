using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState //경계레벨 상태머신
    {
        idle, hide, puzzling, die
    }

    public PlayerState state;

    [SerializeField]
    private float playerspeed;
    [SerializeField]
    private bool handgunacivate = false;
    [SerializeField]
    private bool coinacivate = false;
    [SerializeField]
    private bool flashbangacivate = false;
    [SerializeField]
    private bool heartseeacivate = false;    
    [SerializeField]
    private int armor;
    [SerializeField]
    public bool[] itemGet;

    [SerializeField]
    UseItem useItem;
    IItem item;

    public GameObject handGunModel;
    public GameObject bulletPrefab;
    public GameObject footSound;

    Animator playerAnim;
    string walk = "Walk";
    string handgunMode = "HandGun";
    string throwcoin = "ThrowCoin";
    string throwflashbang = "ThrowFlashBang";
    string run = "Runing";
    string gunrun = "GunRuning";

    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 pos;
    Vector3 mousePos;
    Vector3 velocity;
    bool itemActivate = false;

    void Start()
    {
        playerspeed = 2.5f;
        itemGet =new bool[5] { false,false,false,false,false};
        state = PlayerState.idle;
        useItem = GetComponent<UseItem>();
        rigid = transform.GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        cam = Camera.main;
    }

    void Update()
    {
        if (GameManager.instance.nowpuzzle)
            state = PlayerState.puzzling;
        else if (!GameManager.instance.nowpuzzle)
            state = PlayerState.idle;

        if (state == PlayerState.idle)
            PlayerControll();
        else
            return;

        if (itemGet[4])
            armor = GameManager.instance.itemcount[4];

        
        
    }

    private void FixedUpdate()
    {
        rigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
        rigid.MovePosition(rigid.position + velocity * Time.deltaTime);
    }

    void PlayerControll()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float zDeg = mousePos.z - rigid.position.z;
        float xDeg = mousePos.x - rigid.position.x;
        rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg) - 90);
        //rigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));

        //mousePos = cam.ScreenToWorldPoint(new Vector3
        //    (Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y));
        //transform.LookAt(mousePos + Vector3.up * transform.position.y);

        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;
        //rigid.MovePosition(rigid.position + velocity * Time.deltaTime);

        playerAnim.SetFloat(walk, velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            footSound.SetActive(true);
            playerspeed = 5f;
            if (handgunacivate)
                playerAnim.SetBool(gunrun, true);
            else
                playerAnim.SetBool(run, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            footSound.SetActive(false);
            playerspeed = 2.5f;
            if (handgunacivate)
                playerAnim.SetBool(gunrun, false);
            else
                playerAnim.SetBool(run, false);
        }

        if (itemGet[0])
        {
            ItemActivate1();
            if (handgunacivate && !coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                if(GameManager.instance.itemcount[0]>0)
                    useItem.GunFire(mousePos);
            }
        }
        if (itemGet[1])
        {
            ItemActivate2();
            if (coinacivate)
            {
                useItem.ThrowPosition(coinacivate, flashbangacivate);
            }

            if (!handgunacivate && coinacivate && !flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                if(GameManager.instance.canUse && GameManager.instance.itemcount[1] > 0)
                {
                    playerAnim.SetTrigger(throwcoin);
                    StartCoroutine(useItem.ThrowCoin());
                }
            }
        }
        if (itemGet[2])
        {
            ItemActivate3();
            if (flashbangacivate)
            {
                useItem.ThrowPosition(coinacivate, flashbangacivate);
            }

            if (!handgunacivate && !coinacivate && flashbangacivate && !heartseeacivate && Input.GetMouseButtonDown(0))
            {
                if(GameManager.instance.canUse && GameManager.instance.itemcount[2] > 0)
                {
                    playerAnim.SetTrigger(throwflashbang);
                    StartCoroutine(useItem.ThrowFlashBang());
                }
            }
        }
        if (itemGet[3])
        {
            ItemActivate4();
            if (!handgunacivate && !coinacivate && !flashbangacivate && heartseeacivate && Input.GetMouseButtonDown(0))
            {
                StartCoroutine(useItem.HeartSee());
            }
        }
    }

    void ItemActivate1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !handgunacivate)
        {
            handgunacivate = true;
            handGunModel.SetActive(true);
            Debug.Log("권총 활성화");
            playerAnim.SetBool(handgunMode, true);
            coinacivate = false;
            flashbangacivate = false;
            heartseeacivate = false;
            useItem.ErageDraw();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && handgunacivate)
        {
            playerAnim.SetBool(handgunMode, false);
            handGunModel.SetActive(false);
            handgunacivate = false;
            Debug.Log("권총 비활성화");
            handGunModel.SetActive(false);
        }
    }
    void ItemActivate2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !coinacivate)
        {
            playerAnim.SetBool(handgunMode, false);
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
            useItem.ErageDraw();
        }
    }
    void ItemActivate3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && !flashbangacivate)
        {
            playerAnim.SetBool(handgunMode, false);
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
            useItem.ErageDraw();
        }
    }
    void ItemActivate4()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !heartseeacivate)
        {
            playerAnim.SetBool(handgunMode, false);
            heartseeacivate = true;
            Debug.Log("심장박동측정기 활성화");
            handGunModel.SetActive(false);
            handgunacivate = false;
            coinacivate = false;
            flashbangacivate = false;
            useItem.ErageDraw();
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

            switch(item.value)
            {
                case 1:
                    if (itemGet[0])
                        item.ItemCharge();
                    else
                    {
                        item.GetItem();
                        item.ItemCharge();
                        itemGet[0] = true;
                    }
                    break;
                case 2:
                    if (itemGet[1])
                        item.ItemCharge();
                    else
                    {
                        item.GetItem();
                        item.ItemCharge();
                        itemGet[1] = true;
                    }
                    break;
                case 3:
                    if (itemGet[2])
                        item.ItemCharge();
                    else
                    {
                        item.GetItem();
                        item.ItemCharge();
                        itemGet[2] = true;
                    }
                    break;
                case 4:
                    item.GetItem();
                    itemGet[3] = true;
                    break;
                case 5:
                    if (itemGet[4])
                        item.ItemCharge();
                    else
                    {
                        item.GetItem();
                        item.ItemCharge();
                        itemGet[4] = true;
                    }
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
