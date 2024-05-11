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

    public Text guideUI;                                 // �ڸ� UI
    public GameObject mapPW;

    [SerializeField]
    private GuideData[] guideDatas;               // 

    public int currentDatas_Index;                   // �ҷ��� �ڸ��� �ε����� �ٲ� ����

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

    public void SetOffTxt()                                             // �ڸ� ���ְ� �ؽ�Ʈ �����ִ� �Լ�
    {
        guideUI.gameObject.SetActive(false);
        guideUI.text = null;
    }

    public void SetDifferentTxt(int dataindex)              // ��������
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[dataindex].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt2(int dataindex2)             // ������Ʈ ����
    {
        guideUI.gameObject.SetActive(true);
        guideUI.text = guideLineDB.guideLine[dataindex2].guideTxt;
        Invoke("SetOffTxt", 2.5f);
    }

    public void SetDifferentTxt3()             // ������Ʈ ����
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[10].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
    public void SetDifferentTxt4()             // 2�������� �븻 / �ϵ� ��й�ȣ
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
