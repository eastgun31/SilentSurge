using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DoorMaterialChange : MonoBehaviour
{
    private Material doorMat;
    private Material outlineMat;

    private MeshRenderer dMat;

    void Start()
    {
        dMat = GetComponent<MeshRenderer>();   
    }

    public void DoorMat()
    {
        dMat.material = doorMat;
    }

    public void OutlineMat()
    {
        dMat.material = outlineMat;
    }
    //public Material[] doorMaterial = new Material[2];

    //int i = 0;

    //public void MaterialChange()
    //{
    //    i = ++i % 2;
    //    gameObject.GetComponent<MeshRenderer>().material = doorMaterial[i];
    //}
}
