using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackingPuz : MonoBehaviour
{
    public GameObject hacking;

    [SerializeField] private Text answerInput;
    private int maxNum = 6;

    [SerializeField]
    private string pw = "5VRBZ54K";


    void Update()
    {
        UiManager.instance.TimeRemainig();
    }
}
