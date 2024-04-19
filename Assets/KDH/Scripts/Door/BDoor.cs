using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDoor : MonoBehaviour
{
    public GameObject L_Bdoor;
    public GameObject R_Bdoor;

    public bool bD_Unlock;

    WaitForSeconds bD_openT;

    private void Start()
    {
        bD_openT = new WaitForSeconds(1.1f);
    }

    private void Update()
    {
        BDoorOpen();
    }

    void BDoorOpen()
    {
        if(bD_Unlock)
        {
            StartCoroutine(Bdoor_opening());
        }
    }

    IEnumerator Bdoor_opening()
    {
        L_Bdoor.transform.Translate(0.02f, 0, 0);
        R_Bdoor.transform.Translate(-0.02f, 0, 0);
        yield return bD_openT;
        bD_Unlock = false;
    }
}
