using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlots : MonoBehaviour
{
    public GameObject[] quickSlots;

    private void Update()
    {
        AddSlots();
    }

    public void AddSlots()
    {
        if (GameManager.instance.itemcheck[0])
            quickSlots[0].gameObject.SetActive(true);

        else if (GameManager.instance.itemcheck[1])
            quickSlots[1].gameObject.SetActive(true);

        else if (GameManager.instance.itemcheck[2])
            quickSlots[2].gameObject.SetActive(true);
    }
}
