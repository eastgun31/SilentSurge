using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using DG.Tweening;

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
    private GuideLineDB guideLineDB;          // 

    public Text guideUI;                                 // 자막 UI
    public GameObject mapPW;

    [SerializeField]
    private GuideData[] guideDatas;               // 

    public int currentDatas_Index;                   // 불러올 자막의 인덱스를 바꿀 변수

    public bool isBossOn = true;


    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        for (int i = 0; i < guideLineDB.guideLine.Count; ++i)
            guideDatas[i].guideT = guideLineDB.guideLine[i].guideTxt;

    }

    public void SetOffTxt()                                             // 자막 꺼주고 텍스트 지워주는 함수
    {
        guideUI.gameObject.SetActive(false);
        guideUI.text = null;
    }

    public void SetDifferentTxt(int dataindex)              // 진행유도
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[dataindex].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt2(int dataindex2)             // 오브젝트 관련
    {
        guideUI.gameObject.SetActive(true);
        guideUI.text = guideLineDB.guideLine[dataindex2].guideTxt;
        Invoke("SetOffTxt", 2.5f);
    }

    public void SetDifferentTxt3()             // 오브젝트 관련
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[10].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
    public void SetDifferentTxt4()             // 2스테이지 노말 / 하드 비밀번호
    {
        mapPW.gameObject.SetActive(true);
        guideUI.gameObject.SetActive(true);
        if(GameManager.instance.scenenum == 3)
        {
            guideUI.DOText(guideLineDB.guideLine[13].guideTxt, 1.5f);
            Invoke("SetOffTxt", 2f);
            isBossOn = false;
        }
        if(GameManager.instance.scenenum == 4)
        {
            guideUI.DOText(guideLineDB.guideLine[14].guideTxt, 1.5f);
            Invoke("SetOffTxt", 2f);
            isBossOn = false;
        }
    }

    public void SetDifferentTxt5()
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[15].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }   
    public void SetDifferentTxt6()
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[5].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt7()
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[6].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt8()
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[17].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt9()
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[18].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
}
