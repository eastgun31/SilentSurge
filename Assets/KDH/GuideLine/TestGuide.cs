using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class TestGuide : MonoBehaviour
{
    GuideLineTxt glT;
    private void Start()
    {
        glT=GuideLineTxt.instance;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            glT.SetDifferentTxt(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            glT.SetDifferentTxt(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            glT.SetDifferentTxt(2);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            glT.SetDifferentTxt(3);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            glT.SetOffTxt();
        }
    }
}
