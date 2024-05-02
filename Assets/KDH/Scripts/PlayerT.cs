using ItemInfo;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerT : MonoBehaviour
{
    public enum PlayerState //경계레벨 상태머신
    {
        idle, hide, puzzling, die, find
    }

    public PlayerState state;
    public float distP;

    [SerializeField]
    private float playerspeed;

    //캐싱
    GameManager gmManager;
    SoundManager soundManager;


    float rotDeg;
    Rigidbody rigid;
    Camera cam;
    Vector3 mousePos;
    public Vector3 velocity;
    bool die;
    bool saving;
    public UnityEvent playerDie;

    Ray ray;
    RaycastHit hit;
    int mask;
    string puzzle = "Puzzle";
    string door = "Doorhandle";
    PlayerInteractive playerInteractive;
    public float maxdist;
    void Start()
    {
        playerspeed = 2.5f;
        state = PlayerState.idle;
        rigid = transform.GetComponent<Rigidbody>();
        playerInteractive = GetComponent<PlayerInteractive>();
        cam = Camera.main;
        gmManager = GameManager.instance;
        maxdist = 1f;
    }

    void Update()
    {


        if (state == PlayerState.idle)
        {
            if (rigid.velocity != Vector3.zero)
                rigid.velocity = Vector3.zero;
            PlayerControll();
        }
        else
            return;
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
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * playerspeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position, transform.forward * maxdist, Color.blue, 2f);
            mask = LayerMask.GetMask(door);
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
            {
                distP = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);

                Debug.Log(mask);
                Debug.Log("이름 : " + hit.collider.gameObject.name + "거리는 : " + distP);
                playerInteractive.InteractiveObj(hit);
            }

        }
    }
}
