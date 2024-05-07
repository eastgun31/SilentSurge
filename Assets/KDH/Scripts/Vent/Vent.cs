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
        yield return v1Cooltime;
        v1activate= true;
    }

    IEnumerator V2CoolT()
    {
        yield return v2Cooltime;
        v2activate= true;
    }

    public void V1Cool()
    {
        StartCoroutine(V1CoolT());
    }   
    public void V2Cool()
    {
        StartCoroutine(V2CoolT());
    }
}
