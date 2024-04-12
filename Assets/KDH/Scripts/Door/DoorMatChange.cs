using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMatChange : MonoBehaviour  // 문의 메테리얼을 바꿔주는 스크립트
{
    [SerializeField]
    private Material mat_Door;
    [SerializeField] 
    private Material mat_Outline;

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Doorhandle"))
        {
            this.GetComponent<MeshRenderer>().material = mat_Outline;  // 아웃라인 메테리얼로 0번 배열 변경 
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Doorhandle"))
        {
            this.GetComponent<MeshRenderer>().material = mat_Door;  // 문 기본 메테리얼로 0번 배열 변경
        }
    }

}
