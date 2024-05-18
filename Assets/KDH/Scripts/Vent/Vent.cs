using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Vent : MonoBehaviour
{
    public GameObject vent1;
    public GameObject vent2;

    public bool v1activate = true;
    public bool v2activate = true;
    public bool ventActivate = false;

    WaitForSeconds v1Cooltime;
    WaitForSeconds v2Cooltime;

    private void Start()
    {
        v1Cooltime = new WaitForSeconds(15f);
        v2Cooltime = new WaitForSeconds(15f);
        if(GameManager.instance.scenenum == 5)
        {
            ventActivate = true;
            v1activate = true;
            v2activate = false;
        }
    }

    private void Update()
    {
        Stage3Elv();
    }

    IEnumerator V1CoolT()
    {
            yield return v1Cooltime;
            v1activate = true;
    }

    IEnumerator V2CoolT()
    {
            yield return v2Cooltime;
            v2activate = true;
    }

    public void V1Cool()
    {
            vent1.SetActive(false);
            vent1.SetActive(true);
        if (GameManager.instance.scenenum != 5)
        {
            StartCoroutine(V1CoolT());
        }
    }   
    public void V2Cool()
    {
            vent2.SetActive(false);
            vent2.SetActive(true);
        if (GameManager.instance.scenenum != 5)
        {
            StartCoroutine(V2CoolT());
        }
    }
    public void Stage3Elv()
    {
        if (GameManager.instance.scenenum == 5)
        {
            if (GameManager.instance.puzzleLevel == 2 && !GameManager.instance.clublast)
                v1activate = true;
            
            if (GameManager.instance.clublast == true)
            {
                v1activate = false;
                v2activate = true;
            }
                
        }
    }
}
