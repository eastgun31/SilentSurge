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

    public int isHintRead = 0;

    //���� ����ִ�.�ֺ��� �� �ѷ�����.               - 0
    //������ ���� �� ����.                                      - 1
    //���� ����� �� ����.                                      - 2
    //CCTV �����Ƿ� ������.                                  - 3
    //����� ���踦 ȹ���ߴ�.                                - 4
    //�����ǿ� ��ź�� ��ġ�϶�                             - 5
    //������ ���踦 ȹ���ߴ�.                                - 6
    //�Ϻ� CCTV�� ������.                                      - 7
    //���踦 ȹ���ߴ�.                                            - 8
    //��ź�� ��ġ�ߴ�. ���ѷ� Ż������.                - 9
    //������ ȹ���ߴ�. ���ѷ� Ż������.                - 10
    //Ÿ�ٷ� �����ڵ�� 8324�� �� ����.               - 11
    //Ÿ�ٷ� �����ڵ�� 9635�� �� ����.               - 12
    //��й�ȣ�� 5728�̶� �����ִ�.                     - 13
    //��й�ȣ�� 8015�̶� �����ִ�.                     - 14
    //��ġ�� ������ �����϶�                                 - 15
    //�������� ���ȴ�.                                            - 16
    //��й�ȣ�� ���ϴ� �� ����. �� �ѷ�����       - 17
    //Ÿ�ٷ� ���� �ڵ带 ��ǻ�Ϳ��� ȹ������     - 18
    //���翡�� å�忡 ������ ��ư�� ã�ƺ���     - 19
    //�켱 ��ǻ�Ϳ� �����غ���                             - 20
    //Ÿ���� �����϶�                                             - 21
    //VIP ���� ���� ��ȣ�� 00XX�� ���� �ִ�.       - 22
    //VIP ���� ���� ��ȣ�� XX00�� ���� �ִ�.       - 23
    //�м� ��θ� ȹ���ߴ�.���ѷ� Ż������.        - 24
    //VIP ������ �� ����� ã�ƺ���.             - 25






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

    public void SetDifferentTxt(int dataindex)              // �������� �ڸ� (Ÿ���� ȿ�� (��κ� �ε��� ��))
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[dataindex].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt2(int dataindex2)             // ������Ʈ ���� (0 ,1, 2)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.text = guideLineDB.guideLine[dataindex2].guideTxt;
        Invoke("SetOffTxt", 2.5f);
    }

    public void SetDifferentTxt3()            // 1�������� ���� óġ �� �ڸ� (�̺�Ʈ)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[10].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
    public void SetDifferentTxt4()             // 2�������� �븻 / �ϵ� ��й�ȣ (�̺�Ʈ)
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

    public void SetDifferentTxt5()              // 3�������� ���� ���̰� ���� �ڸ� (�̺�Ʈ)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[24].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
}
