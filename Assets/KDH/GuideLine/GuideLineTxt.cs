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

    [SerializeField]
    private GuideData[] guideDatas;               // 

    public int currentDatas_Index;                   // �ҷ��� �ڸ��� �ε����� �ٲ� ����

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
}
