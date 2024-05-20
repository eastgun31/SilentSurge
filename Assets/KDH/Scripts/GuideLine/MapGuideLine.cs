using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGuideLine : MonoBehaviour
{
    public int guideN;

    public GameObject MapPass3;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.scenenum == 1 || GameManager.instance.scenenum == 2)
            {
                if (guideN == 0)
                {
                    GuideLineTxt.instance.SetDifferentTxt(20);
                    this.gameObject.SetActive(false);
                }   // 1스테이지 입장 하고 움직일 때
                if (guideN == 1)
                {
                    GuideLineTxt.instance.SetDifferentTxt(3);
                    this.gameObject.SetActive(false);
                }   // CCTV 유도 문구
                if (guideN == 2 && GameManager.instance.puzzleLevel >= 3)
                {
                    GuideLineTxt.instance.SetDifferentTxt(6);
                    this.gameObject.SetActive(false);
                }   // 1스테이지 서버실 유도 문구
                if (guideN == 3 && GameManager.instance.puzzleLevel < 5)
                {
                    GuideLineTxt.instance.SetDifferentTxt(18);
                    this.gameObject.SetActive(false);
                }   // 1스테이지 큰방 입장 시 나올 문구 (GetHint)
            }
            if (GameManager.instance.scenenum == 3 || GameManager.instance.scenenum == 4)
            {
                if (guideN == 0)
                {
                    GuideLineTxt.instance.SetDifferentTxt(19);
                    this.gameObject.SetActive(false);
                }   // 2스테이지 서재에서 책장에 퍼즐 버튼 있다는 문구
                if (guideN == 1 && GameManager.instance.puzzleLevel == 2)
                {
                    GuideLineTxt.instance.SetDifferentTxt(21);
                    this.gameObject.SetActive(false);
                }   // 2스테이지 큰 문 열리고 나서 나올 문구 (GoTarget)
                if (guideN == 2 && !GuideLineTxt.instance.isBossOn)
                {
                    GuideLineTxt.instance.SetDifferentTxt(15);
                    this.gameObject.SetActive(false);
                }   // 2스테이지 인질 구출 
                if (guideN == 3 && !GuideLineTxt.instance.isBossOn)
                {
                    GuideLineTxt.instance.SetDifferentTxt(5);
                    this.gameObject.SetActive(false);
                }   // 2스테이지 인질 구출하고 폭탄 설치 안내 문구
            }
            if(GameManager.instance.scenenum == 5)
            {
                if(guideN == 0) 
                {
                    GuideLineTxt.instance.SetDifferentTxt(25);
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
