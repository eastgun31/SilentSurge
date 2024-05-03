using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    public void SetOffTxt()
    {
        guideUI.gameObject.SetActive(false);
    }

    public void SetDifferentTxt()
    {
        guideUI.text = guideLineDB.guideLine[currentDatas_Index].guideTxt;
        guideUI.gameObject.SetActive(true);
    }

}
