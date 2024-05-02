using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct GuideData
{
    public int guideText_Index;
    public string guideT;
}

[System.Serializable]
public struct GuideText
{
    public Text guideText;                            // ÀÚ¸·
}


public class GuideLineTxt : MonoBehaviour
{
    [SerializeField]
    private int eventNum;                              //
    [SerializeField]
    private GuideLineDB guideLineDB;          //


    [SerializeField]
    private GuideData[] guideDatas;               //
    [SerializeField]
    private GuideText[] guideTexts;                 // 


    private int currentDatas_Index = -1;           //
    private int currentTexts_Index = 0;             //


    void Awake()
    {
        int index = 0;
        for (int i = 0; i < guideLineDB.guideLine.Count; ++i)
        {
            if (guideLineDB.guideLine[i].eventNum == eventNum)
            {
                guideDatas[index].guideT = guideLineDB.guideLine[i].guideTxt;
                index++;
            }
        }
        GuideTxtSetting();
    }

    void Update()
    {
        
    }

    public void GuideTxtSetting()
    {
        for(int i = 0; i<guideTexts.Length; ++i)
        {
            SetActiveTxt(guideTexts[i],false);
        }
    }

    public void SetActiveTxt(GuideText guideT, bool visible)
    {
        guideT.guideText.gameObject.SetActive(visible);
    }

    public void SetOffTxt()
    {
        SetActiveTxt(guideTexts[currentTexts_Index], false);
    }

    public void SetDifferentTxt()
    {
        currentTexts_Index = guideDatas[currentDatas_Index].guideText_Index;
        SetActiveTxt(guideTexts[currentTexts_Index], true);
        guideTexts[currentTexts_Index].guideText.text = guideDatas[currentDatas_Index].guideT;
    }
}
