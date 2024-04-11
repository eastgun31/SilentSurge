using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour     // ��ȣ�ۿ� ������ ������Ʈ�� �������� �� �ش� ������Ʈ�� �ƿ������� �׷��ִ� ��ũ��Ʈ
{
    [SerializeField]
    private Material orgMat;
    [SerializeField]
    private Material outlineMat;



    private void OnTriggerStay(Collider other)
    {
        this.GetComponent<MeshRenderer>().material = outlineMat;
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<MeshRenderer>().material = orgMat;
    }
}
