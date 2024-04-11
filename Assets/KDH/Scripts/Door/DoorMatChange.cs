using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMatChange : MonoBehaviour  // ���� ���׸����� �ٲ��ִ� ��ũ��Ʈ
{
    [SerializeField]
    private Material mat_Door;
    [SerializeField] 
    private Material mat_Outline;

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Doorhandle"))
        {
            this.GetComponent<MeshRenderer>().material = mat_Outline;  // �ƿ����� ���׸���� 0�� �迭 ���� 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Doorhandle"))
        {
            this.GetComponent<MeshRenderer>().material = mat_Door;  // �� �⺻ ���׸���� 0�� �迭 ����
        }
    }

}
