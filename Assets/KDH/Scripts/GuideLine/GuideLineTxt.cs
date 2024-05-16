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

    public int isHintRead = 0;

    //문이 잠겨있다.주변을 더 둘러보자.               - 0
    //지금은 숨을 수 없다.                                      - 1
    //아직 사용할 수 없다.                                      - 2
    //CCTV 관리실로 가보자.                                  - 3
    //전기실 열쇠를 획득했다.                                - 4
    //가스실에 폭탄을 설치하라                             - 5
    //서버실 열쇠를 획득했다.                                - 6
    //일부 CCTV가 꺼졌다.                                      - 7
    //열쇠를 획득했다.                                            - 8
    //폭탄을 설치했다. 서둘러 탈출하자.                - 9
    //정보를 획득했다. 서둘러 탈출하자.                - 10
    //타겟룸 접근코드는 8324인 것 같다.               - 11
    //타겟룸 접근코드는 9635인 것 같다.               - 12
    //비밀번호가 5728이라 적혀있다.                     - 13
    //비밀번호가 8015이라 적혀있다.                     - 14
    //납치된 인질을 구출하라                                 - 15
    //막힌문이 열렸다.                                            - 16
    //비밀번호를 뜻하는 것 같다. 더 둘러보자       - 17
    //타겟룸 접근 코드를 컴퓨터에서 획득하자     - 18
    //서재에서 책장에 숨겨진 버튼을 찾아보자     - 19
    //우선 컴퓨터에 접근해보자                             - 20
    //타겟을 제거하라                                             - 21
    //VIP 구역 입장 번호가 00XX라 적혀 있다.       - 22
    //VIP 구역 입장 번호가 XX00라 적혀 있다.       - 23
    //밀수 장부를 획득했다.서둘러 탈출하자.        - 24
    //VIP 구역을 들어갈 방법을 찾아보자.             - 25






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

    public void SetDifferentTxt(int dataindex)              // 진행유도 자막 (타이핑 효과 (대부분 인덱스 값))
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[dataindex].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }

    public void SetDifferentTxt2(int dataindex2)             // 오브젝트 관련 (0 ,1, 2)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.text = guideLineDB.guideLine[dataindex2].guideTxt;
        Invoke("SetOffTxt", 2.5f);
    }

    public void SetDifferentTxt3()            // 1스테이지 보스 처치 후 자막 (이벤트)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[10].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
    public void SetDifferentTxt4()             // 2스테이지 노말 / 하드 비밀번호 (이벤트)
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

    public void SetDifferentTxt5()              // 3스테이지 보스 죽이고 나올 자막 (이벤트)
    {
        guideUI.gameObject.SetActive(true);
        guideUI.DOText(guideLineDB.guideLine[24].guideTxt, 1.5f);
        Invoke("SetOffTxt", 3.5f);
    }
}
