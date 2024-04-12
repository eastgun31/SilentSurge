using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour     // 상호작용 가능한 오브젝트에 인접했을 때 해당 오브젝트에 아웃라인을 그려주는 스크립트
{
    public Material[] pc_mat;
    public Material outline_mat;


















    //----------------------------------------------------------------------------------------------

    //[SerializeField]
    //private Material orgMat;
    //[SerializeField]
    //private Material outlineMat;


    //private void OnTriggerStay(Collider other)
    //{
    //    this.GetComponentInChildren<MeshRenderer>().material = outlineMat;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    this.GetComponentInChildren<MeshRenderer>().material = orgMat;
    //}
}
