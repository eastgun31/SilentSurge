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
        if(GameManager.instance.scenenum == 5)
        {
            if (GameManager.instance.puzzleLevel == 2)
                v1activate = true;
            else if (GameManager.instance.last == true)
                v2activate = true;
        }
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
        if(GameManager.instance.scenenum != 5)
        {
            vent1.SetActive(false);
            vent1.SetActive(true);
            StartCoroutine(V1CoolT());
        }
    }   
    public void V2Cool()
    {
        if(GameManager.instance.scenenum != 5)
        {
            vent2.SetActive(false);
            vent2.SetActive(true);
            StartCoroutine(V2CoolT());
        }
    }
}
