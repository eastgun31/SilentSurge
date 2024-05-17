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
                }   // 1�������� ���� �ϰ� ������ ��
                if (guideN == 1)
                {
                    GuideLineTxt.instance.SetDifferentTxt(3);
                    this.gameObject.SetActive(false);
                }   // CCTV ���� ����
                if (guideN == 2 && GameManager.instance.puzzleLevel >= 3)
                {
                    GuideLineTxt.instance.SetDifferentTxt(6);
                    this.gameObject.SetActive(false);
                }   // 1�������� ������ ���� ����
                if (guideN == 3 && GameManager.instance.puzzleLevel < 5)
                {
                    GuideLineTxt.instance.SetDifferentTxt(18);
                    this.gameObject.SetActive(false);
                }   // 1�������� ū�� ���� �� ���� ���� (GetHint)
            }
            if (GameManager.instance.scenenum == 3 || GameManager.instance.scenenum == 4)
            {
                if (guideN == 0)
                {
                    GuideLineTxt.instance.SetDifferentTxt(19);
                    this.gameObject.SetActive(false);
                }   // 2�������� ���翡�� å�忡 ���� ��ư �ִٴ� ����
                if (guideN == 1 && GameManager.instance.puzzleLevel == 2)
                {
                    GuideLineTxt.instance.SetDifferentTxt(21);
                    this.gameObject.SetActive(false);
                }   // 2�������� ū �� ������ ���� ���� ���� (GoTarget)
                if (guideN == 2 && !GuideLineTxt.instance.isBossOn)
                {
                    GuideLineTxt.instance.SetDifferentTxt(15);
                    this.gameObject.SetActive(false);
                }   // 2�������� ���� ���� 
                if (guideN == 3 && !GuideLineTxt.instance.isBossOn)
                {
                    GuideLineTxt.instance.SetDifferentTxt(5);
                    this.gameObject.SetActive(false);
                }   // 2�������� ���� �����ϰ� ��ź ��ġ �ȳ� ����
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
