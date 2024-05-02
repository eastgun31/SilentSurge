using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Events;


[System.Serializable]
public class SaveData
{
    public Player player;
    public bool[] playeritemget = new bool[5];
    public Vector3 playerposition;
    public Vector3 playerrotation;

    public bool[] gmitemcheck = new bool[5];
    public int[] gmitemcount = new int[5]{0,0,0,0,0};
    public bool[] gmexistitem = new bool[5];
    public bool[] gmexistenemy;
    //public bool[] gmexistenemy2 = new bool[12];
    public int gmpuzzleLevel;
    public bool gmnowpuzzle;
    public bool gmcanUse;
    public bool gmplayerchasing;

    public bool _handgunacivate;
    public bool _coinacivate;
    public bool _flashbangacivate;
    public bool _heartseeacivate;
    public bool heartseecool;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public UnityEvent playerLoad;
    public GameObject playerobj;

    public GameObject gameOver;

    private Player player;
    //public bool[] playeritemget;
    //public Vector3 playerposition;

    //public bool[] gmitemcheck;
    //public int[] gmitemcount;
    //public int gmpuzzleLevel;
    //public bool gmnowpuzzle;
    //public bool gmcanUse;
    //public bool gmplayerchasing;

    public string savename = "/save";
    private SaveData saveData = new SaveData();
    private string SAVEDAT;
    WaitForSeconds wait;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        //DontDestroyOnLoad(this.gameObject);

        SAVEDAT = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVEDAT)) // 해당 경로가 존재하지 않는다면
            Directory.CreateDirectory(SAVEDAT); // 폴더 생성(경로 생성)
    }
    private void Start()
    {
        wait = new WaitForSeconds(1f);
        saveData.gmexistenemy = new bool[GameManager.instance.existEnemy.Length];
        saveData.gmexistitem = new bool[GameManager.instance.existItem.Length];
        Invoke("SaveData", 2f);
    }

    public void SaveData()
    {
        player = FindObjectOfType<Player>();
        saveData.playerposition = player.transform.position;
        saveData.playerrotation = player.transform.rotation.eulerAngles;
        for (int i = 0; i < player.itemGet.Length; i++)
        {
            saveData.playeritemget[i] = player.itemGet[i];
        }

        for (int i = 0; i < GameManager.instance.itemcheck.Length; i++)
        {
            saveData.gmitemcheck[i] = GameManager.instance.itemcheck[i];
        }
        for (int i = 0; i < GameManager.instance.itemcount.Length; i++)
        {
            saveData.gmitemcount[i] = GameManager.instance.itemcount[i];
        }
        for (int i = 0; i < GameManager.instance.existItem.Length; i++)
        {
            saveData.gmexistitem[i] = GameManager.instance.existItem[i];
        }
        for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        {
            saveData.gmexistenemy[i] = GameManager.instance.existEnemy[i];
        }
        //if (GameManager.instance.scenenum ==1)
        //{
        //    for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        //    {
        //        saveData.gmexistenemy[i] = GameManager.instance.existEnemy[i];
        //    }
        //}
        //else if (GameManager.instance.scenenum == 2)
        //{
        //    for (int i = 0; i < GameManager.instance.existEnemy.Length; i++)
        //    {
        //        saveData.gmexistenemy[i] = GameManager.instance.existEnemy[i];
        //    }
        //}

        saveData._handgunacivate = player.handgunacivate;
        saveData._coinacivate = player.coinacivate;
        saveData._flashbangacivate = player.flashbangacivate;
        saveData._heartseeacivate = player.heartseeacivate;

        saveData.gmpuzzleLevel = GameManager.instance.puzzleLevel;
        saveData.gmnowpuzzle = GameManager.instance.nowpuzzle;
        saveData.gmcanUse = GameManager.instance.canUse;
        //saveData.gmplayerchasing = GameManager.instance.playerchasing;

        string json = JsonUtility.ToJson(saveData);
        string filePath = SAVEDAT + savename;
        File.WriteAllText(filePath, json);
        Debug.Log("세이브완료");
    }
    
    public void LoadData()
    {
        string filePath = SAVEDAT + savename;

        if (File.Exists(filePath))
        {
            gameOver.SetActive(false);

            string loadJson = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            GameManager.instance.isDie = false;
            playerobj = GameObject.FindWithTag("Player");            
            player = FindObjectOfType<Player>();
            playerobj.SetActive(false);
            player.transform.position = saveData.playerposition;
            player.transform.eulerAngles = saveData.playerrotation;

            for (int i = 0; i < saveData.playeritemget.Length; i++)
            {
                player.itemGet[i] = saveData.playeritemget[i];
            }

            for (int i = 0; i < saveData.gmitemcheck.Length; i++)
            {
                GameManager.instance.itemcheck[i] = saveData.gmitemcheck[i];
            }
            for (int i = 0; i < saveData.gmitemcount.Length; i++)
            {
                GameManager.instance.itemcount[i] = saveData.gmitemcount[i];
            }
            for (int i = 0; i < saveData.gmexistitem.Length; i++)
            {
                GameManager.instance.existItem[i] = saveData.gmexistitem[i];
            }
            for (int i = 0; i < saveData.gmexistenemy.Length; i++)
            {
                GameManager.instance.existEnemy[i] = saveData.gmexistenemy[i];
            }
            //if (GameManager.instance.scenenum == 1)
            //{
            //    for (int i = 0; i < saveData.gmexistenemy.Length; i++)
            //    {
            //        GameManager.instance.existEnemy[i] = saveData.gmexistenemy[i];
            //    }
            //}
            //else if (GameManager.instance.scenenum == 2)
            //{
            //    for (int i = 0; i < saveData.gmexistenemy.Length; i++)
            //    {
            //         GameManager.instance.existEnemy[i] = saveData.gmexistenemy[i];
            //    }
            //}

            player.handgunacivate = saveData._handgunacivate;
            player.coinacivate = saveData._coinacivate;
            player.flashbangacivate = saveData._flashbangacivate;
            player.heartseeacivate = saveData._heartseeacivate;

            GameManager.instance.puzzleLevel = saveData.gmpuzzleLevel;
            GameManager.instance.nowpuzzle = saveData.gmnowpuzzle;
            GameManager.instance.canUse = saveData.gmcanUse;
            //GameManager.instance.playerchasing = saveData.gmplayerchasing;

            playerobj.SetActive(true);
            GameManager.instance.isDie = false;
            Debug.Log("로드완료");
            playerLoad.Invoke();
        }
    }

    IEnumerator SaveDelay()
    {
        yield return wait;
        Debug.Log("세이브중");
    }
}
