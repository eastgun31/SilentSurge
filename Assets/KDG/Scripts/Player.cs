using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public enum PlayerState //경계레벨 상태머신
    {
        idle, hide, puzzling, die, find
    }

    public PlayerState state;

    [SerializeField]
    private float playerspeed;

    public bool handgunacivate = false;
    public bool coinacivate = false;
    public bool flashbangacivate = false;
    public bool heartseeacivate = false;
    public bool heartseecool = true;

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

    //캐싱
    GameManager gmManager;
    SoundManager soundManager;
    UiManager uiManager;
    public Animator playerAnim;
    string walk = "Walk";
    string gunwalk = "GunWalk";
    string handgunMode = "HandGun";
    string throwcoin = "ThrowCoin";
    string throwflashbang = "ThrowFlashBang";
    string run = "Runing";
    string _run = "_Runing";
    string gunrun = "GunRuning";
    string _gunrun = "_GunRuning";
    

    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 mousePos;
    public Vector3 velocity;
    bool die;
    bool saving;
    public UnityEvent playerDie;

    //레이캐스트
    Ray ray;
    RaycastHit hit;
    int mask;
    string puzzle = "Puzzle";
    string door = "Doorhandle";
    string enemy = "Enemy";
    public bool canAmsal = false; //암살가능 체크변수
    PlayerInteractive playerInteractive;
    public Sight sight;
    Enemy enemyState;
    public float maxdist;

    void Start()
    {
        die = false;
        saving = false;
        playerspeed = 2.5f;
        itemGet =new bool[5] { false,false,false,false,false};
        state = PlayerState.idle;
        useItem = GetComponent<UseItem>();
        rigid = transform.GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerInteractive = GetComponent<PlayerInteractive>();
        cam = Camera.main;
        gmManager = GameManager.instance;
        soundManager = SoundManager.instance;
        uiManager = UiManager.instance;
        maxdist = 1f;
    }

    void Update()
    {
        //Debug.Log(math.round(velocity.magnitude));
        if (gmManager.nowpuzzle)
        {            
            //playerAnim.SetBool(gunrun, false);
            //playerAnim.SetBool(run, false);
            footSound.SetActive(false);
            state = PlayerState.puzzling;

            //playerspeed = 0;
        }
        else if (gmManager.isHide)
        {
            state = PlayerState.hide;
            useItem.ErageDraw();
        }        
        else if (gmManager.isDie)
        {
            state = PlayerState.die;
            rigid.velocity = Vector3.zero;
        }
        else if (!gmManager.nowpuzzle || !gmManager.isHide || !gmManager.isDie)
        {
            state = PlayerState.idle;
            //playerspeed = 2.5f;
        }
           
        if (state == PlayerState.idle && !uiManager.isPauseWin)
        {
            if (rigid.velocity != Vector3.zero)
                rigid.velocity = Vector3.zero;
            PlayerControll();
        }
        else
            return;

        if (itemGet[4])
            armor = gmManager.itemcount[4];

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("11");
            useItem.OnOffMap();
        }
    }

    private void FixedUpdate()
    {
        rigid.MoveRotation(Quaternion.Euler(0, rotDeg, 0));
        rigid.MovePosition(rigid.position + velocity * Time.deltaTime);
    }

    void PlayerMoveAnim()
    {
        
        //playerAnim.SetFloat(walk,velocity.magnitude);

        if(velocity.magnitude >= 5f && handgunacivate)
            playerAnim.SetFloat(_gunrun, velocity.magnitude);
        else if(velocity.magnitude >= 5f)
            playerAnim.SetFloat(_run, velocity.magnitude);
        else
            playerAnim.SetFloat(walk, velocity.magnitude);
    }

    void PlayerControll()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float zDeg = mousePos.z - rigid.position.z;
        float xDeg = mousePos.x - rigid.position.x;
        rotDeg = -(Mathf.Rad2Deg * Mathf.Atan2(zDeg, xDeg) - 90);
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;
        playerAnim.SetFloat(walk, velocity.magnitude);

        //PlayerMoveAnim();

        if (itemGet[0])
        {
            ItemActivate1();
            if (handgunacivate && Input.GetMouseButtonDown(0))
            {
                if(gmManager.itemcount[0]>0)
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

            if ( coinacivate && Input.GetMouseButtonDown(0))
            {
                if(gmManager.canUse && gmManager.itemcount[1] > 0)
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

            if (flashbangacivate && Input.GetMouseButtonDown(0))
            {
                if(gmManager.canUse && gmManager.itemcount[2] > 0)
                {
                    playerAnim.SetTrigger(throwflashbang);
                    StartCoroutine(useItem.ThrowFlashBang());
                }
            }
        }
        if (itemGet[3])
        {
            ItemActivate4();
            if ( heartseeacivate && Input.GetMouseButtonDown(0) && useItem.heartCanUse)
            {
                StartCoroutine(useItem.HeartSee());
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position, transform.forward * maxdist, Color.blue, 2f);
            mask = LayerMask.GetMask(puzzle) | LayerMask.GetMask(door);
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxdist, mask))
            {
                Debug.Log(mask);
                playerInteractive.InteractiveObj(hit);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerspeed = 5f;
            playerAnim.SetFloat(walk, 0);

            if(handgunacivate)
                playerAnim.SetFloat(_gunrun,velocity.magnitude);
            else
                playerAnim.SetFloat(_run, velocity.magnitude);
        }
        if (Input.GetKey(KeyCode.LeftShift) && velocity.magnitude >= 5)
        {
            //if(velocity.magnitude < 5)
            //    footSound.SetActive(false);

            //Debug.Log("달리기");
            footSound.SetActive(true);

            if (soundManager.effectPlayer.isPlaying)
                return;
            else if (!soundManager.effectPlayer.isPlaying)
                soundManager.EffectPlay(0, true, 1f);

            //if (handgunacivate)
            //    playerAnim.SetBool(gunrun, true);
            //else
            //    playerAnim.SetBool(run, true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerspeed = 2.5f;
            RunOff();
        }

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, 1f, LayerMask.GetMask(enemy)))
        {
            sight = hit.transform.gameObject.GetComponent<Sight>();
            enemyState = hit.transform.transform.gameObject.GetComponent<Enemy>();
            if (!sight.findT && enemyState.state != Enemy.EnemyState.die && !handgunacivate && !coinacivate && !flashbangacivate && !heartseeacivate)
            {
                canAmsal = true;
                Debug.Log("암살가능");
                if (canAmsal  && Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(useItem.Assassination());
                    sight = null;
                    Debug.Log("암살불능");
                    canAmsal = false;
                }
                    
            }
        }
        else
        {
            if (canAmsal)
            {
                Debug.Log("암살불능");
                canAmsal = false;
            }
            else
                return;
        }

        //if (!die && Input.GetKey(KeyCode.G))
        //{
        //    die = true;
        //    StartCoroutine(PlayerDie());
        //}
        //if (!saving && Input.GetKey(KeyCode.F))
        //{
        //    saving = true;
        //    StartCoroutine(PlayerSave());
        //}
    }

    void ItemActivate1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !handgunacivate)
        {
            if(gmManager.scenenum != 5)
                transform.position = new Vector3(transform.position.x, 0.01f, transform.position.z);
            handGunModel.SetActive(true);
            Debug.Log("권총 활성화");
            //playerAnim.SetBool(run, false);
            playerAnim.SetBool(handgunMode, true);

            ItemActivateControll(true, false, false, false);
            useItem.ErageDraw();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && handgunacivate)
        {
            playerAnim.SetBool(handgunMode, false);
            //playerAnim.SetBool(gunrun, false);
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
            //playerAnim.SetBool(gunrun, false);
            Debug.Log("코인 활성화");
            handGunModel.SetActive(false);

            ItemActivateControll(false, true, false, false);
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
            //playerAnim.SetBool(gunrun, false);
            Debug.Log("섬광탄 활성화");
            handGunModel.SetActive(false);

            ItemActivateControll(false, false, true, false);
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
            //playerAnim.SetBool(gunrun, false);
            Debug.Log("심장박동측정기 활성화");
            handGunModel.SetActive(false);

            ItemActivateControll(false, false, false, true);
            useItem.ErageDraw();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && heartseeacivate)
        {
            heartseeacivate = false;
            Debug.Log("심장박동측정기 비활성화");
        }
    }
    void ItemActivateControll(bool a,bool b, bool c, bool d)
    {   
        handgunacivate = a;
        coinacivate = b;
        flashbangacivate = c;
        heartseeacivate = d;
        
    }

    public void RunOff()
    {
        playerAnim.SetFloat(walk, 0);
        playerAnim.SetFloat(_run, 0);
        playerAnim.SetFloat(_gunrun, 0);
        velocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
        footSound.SetActive(false);
        soundManager.EffectOff();
        //playerAnim.SetBool(run, false);
        //playerAnim.SetBool(gunrun, false);
        
    }


    IEnumerator PlayerDie()
    {
        gmManager.isDie = true;
        gmManager.isGameOver = true;
        ItemActivateControll(false, false, false, false);
        handGunModel.SetActive(false);
        RunOff();
        useItem.ItemOff();
        playerAnim.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        Debug.Log("플레이어 죽음");
        //DataManager.instance.LoadData();
        playerDie.Invoke();
        gmManager.isGameOver = false;
        die = false;
    }
    IEnumerator PlayerSave()
    {
        yield return new WaitForSeconds(2f);
        DataManager.instance.SaveData();
        saving = false;
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
            gmManager.existItem[item.indexNum] = false;
            other.gameObject.SetActive(false);
        }
        if(other.CompareTag("E_Bullet"))
        {
            if (itemGet[4] && gmManager.itemcount[4] >0)
            {
                gmManager.itemcount[4]--;
            }
            else
            {
                if(state != PlayerState.die)
                    StartCoroutine(PlayerDie());
            }
        }
    }
}
