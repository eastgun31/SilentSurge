using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (!v1activate)
            StartCoroutine(V1CoolT());
        if (!v2activate)
            StartCoroutine(V2CoolT());
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

}
