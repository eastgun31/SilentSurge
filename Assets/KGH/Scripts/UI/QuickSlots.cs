using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlots : MonoBehaviour
{
    public GameObject[] quickSlots;
    public GameObject[] c_text;

    

    public Text[] i_count;

    private void Update()
    {
        AddSlots();
    }

    public void AddSlots()
    {
        if (GameManager.instance.itemcheck[0])       //±ÇÃÑ Ãß°¡
        {
            quickSlots[0].gameObject.SetActive(true);
            c_text[0].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[0] >= 1)
            {
                i_count[0].text = GameManager.instance.itemcount[0].ToString();
            }
            else if (GameManager.instance.itemcount[0] == 0)
            {
                c_text[0].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[1])       //ÄÚÀÎ Ãß°¡
        {
            quickSlots[1].gameObject.SetActive(true);
            c_text[1].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[1] >= 1)
            {
                i_count[1].text = GameManager.instance.itemcount[1].ToString();
            }
            else if (GameManager.instance.itemcount[1] == 0)
            {
                c_text[1].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[2])      // ¼¶±¤ÅºÃß°¡
        {
            quickSlots[2].gameObject.SetActive(true);
            c_text[2].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[2] >= 1)
            {
                i_count[2].text = GameManager.instance.itemcount[2].ToString();
            }
            else if (GameManager.instance.itemcount[2] == 0)
            {
                c_text[2].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[3])        // ½É¹Ú±âÃß°¡
            quickSlots[3].gameObject.SetActive(true);

        if (GameManager.instance.itemcheck[4])       // ¹æÅºº¹
        {
            quickSlots[4].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[4] == 3)
                Debug.Log("White");
            else if (GameManager.instance.itemcount[4] == 2)
                Debug.Log("yellow");
            else if (GameManager.instance.itemcount[4] == 1)
                Debug.Log("Red");
            else if (GameManager.instance.itemcount[4] == 0)
            {
                quickSlots[4].gameObject.SetActive(false);
            }
        }
    }
}
