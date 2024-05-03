using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
//using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public struct GuideData
{
    public int guideText_Index;
    public string guideT;
}

public class GuideLineTxt : MonoBehaviour
{
    public static GuideLineTxt instance;

    [SerializeField]
    private int eventNum;                              // 
    [SerializeField]
    private GuideLineDB guideLineDB;          // 

    public Text guideUI;                                 // 자막 UI

    //public UnityEvent guideAnim;

    [SerializeField]
    private GuideData[] guideDatas;               // 

    public int currentDatas_Index;                   // 불러올 자막의 인덱스를 바꿀 변수

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        for (int i = 0; i < guideLineDB.guideLine.Count; ++i)
            guideDatas[i].guideT = guideLineDB.guideLine[i].guideTxt;

    }

    public void SetOffTxt()
    {
        guideUI.gameObject.SetActive(false);
    }

    public void SetDifferentTxt(int dataindex)
    {
        //guideAnim.Invoke();
        guideUI.gameObject.SetActive(true);
        guideUI.text = guideLineDB.guideLine[dataindex].guideTxt;
        Invoke("SetOffTxt", 2.5f);
    }
}
