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
    }

    IEnumerator V1CoolT()
    {
        Debug.Log("000000");
        yield return v1Cooltime;
        Debug.Log("11111111");
        v1activate = true;
    }

    IEnumerator V2CoolT()
    {
        Debug.Log("222222");
        yield return v2Cooltime;
        Debug.Log("333333");
        v2activate= true;
    }

    public void V1Cool()
    {
        vent1.SetActive(false);
        vent1.SetActive(true);
        StartCoroutine(V1CoolT());
    }   
    public void V2Cool()
    {
        vent2.SetActive(false);
        vent2.SetActive(true);
        StartCoroutine(V2CoolT());
    }
}
