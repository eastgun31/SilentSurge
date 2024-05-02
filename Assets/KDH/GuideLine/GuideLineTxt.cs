using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct GuidetxtData
{
    public int guideIndex;
    public string guideTxt;
}

[System.Serializable]
public struct GuideTxt
{
    public Text guideTxtText;
}

public class GuideLineTxt : MonoBehaviour
{
    [SerializeField]
    private int eventNum;
    [SerializeField]
    private GuideLineDB guideLineDB;

    private void Start()
    {
        
    }
}
