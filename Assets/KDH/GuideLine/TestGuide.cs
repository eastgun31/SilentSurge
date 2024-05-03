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
            glT.currentDatas_Index = 0;
            glT.SetDifferentTxt();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            glT.currentDatas_Index = 1;
            glT.SetDifferentTxt();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            glT.currentDatas_Index = 2;
            glT.SetDifferentTxt();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            glT.currentDatas_Index = 3;
            glT.SetDifferentTxt();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            glT.SetOffTxt();
        }
    }
}
