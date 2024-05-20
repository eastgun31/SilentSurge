using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public Light light1;
    public Light light2;

    float lightAngle;

    WaitForSeconds lightChange;

    private void Start()
    {
        lightChange = new WaitForSeconds(0.5f);
        StartCoroutine(ClubLight());
    }

    IEnumerator ClubLight()
    {
        while (true) 
        {
            light1.range = UnityEngine.Random.Range(6, 16);
            light2.range = UnityEngine.Random.Range(6, 16);
            yield return lightChange;
        }
    }
}
