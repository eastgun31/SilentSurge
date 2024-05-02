using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct GuidetxtData
{
    public int guideIndex;
    public string guideTxt;
}

[System.Serializable]
public struct GuideTxt
{
    public Text guideTxtText;
}

public class GuideLineTxt : MonoBehaviour
{
    [SerializeField]
    private int eventNum;
    [SerializeField]
    private GuideLineDB guideLineDB;

    [SerializeField]
    private GuidetxtData[] guidetxtDatas;
    [SerializeField]
    private GuideTxt[] guideTxts;

    private int currentguidetxtDatasIndex;
    private int currentguideTxtsIndex;


    //void Awake()
    //{
    //    int index = 0;
    //    for (int i = 0; i < guideLineDB.guideLine.Count; ++i)
    //    {
    //        if (guideLineDB.guideLine[i].eventNum == eventNum)
    //        {

    //        }
    //    }

    //    GuideTxtSetting();
    //}

    void Update()
    {
        
    }

    public void GuideTxtSetting()
    {
        for(int i = 0; i<guideTxts.Length; ++i)
        {
            SetActiveTxt(guideTxts[i],false);
        }
    }

    public void SetActiveTxt(GuideTxt guidetxt, bool visible)
    {
        guidetxt.guideTxtText.gameObject.SetActive(visible);
    }
}
