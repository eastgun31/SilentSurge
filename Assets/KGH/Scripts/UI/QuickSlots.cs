using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlots : MonoBehaviour
{
    public GameObject[] quickSlots;
    public GameObject[] c_text;

    public Image[] i_color;

    public Text[] i_count;

    
    private void Update()
    {
        AddSlots();
    }

    public void AddSlots()
    {

        if (GameManager.instance.itemcheck[0])       //���� �߰�
        {
            quickSlots[0].gameObject.SetActive(true);
            c_text[0].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[0] >= 1)
            {
                i_count[0].text = GameManager.instance.itemcount[0].ToString();
            }
            else if (GameManager.instance.itemcount[0] == 0)
            {
                GameObject.Find("HandGun").GetComponent<Image>().color = new Color(140, 140, 140);
                c_text[0].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[1])       //���� �߰�
        {
            quickSlots[1].gameObject.SetActive(true);
            c_text[1].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[1] >= 1)
            {
                i_count[1].text = GameManager.instance.itemcount[1].ToString();
            }
            else if (GameManager.instance.itemcount[1] == 0)
            {
                GameObject.Find("Coin").GetComponent<Image>().color = new Color(140, 140, 140);
                c_text[1].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[2])      // ����ź�߰�
        {
            quickSlots[2].gameObject.SetActive(true);
            c_text[2].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[2] >= 1)
            {
                i_count[2].text = GameManager.instance.itemcount[2].ToString();
            }
            else if (GameManager.instance.itemcount[2] == 0)
            {
                GameObject.Find("Flashbang").GetComponent<Image>().color = new Color(140, 140, 140);
                c_text[2].gameObject.SetActive(false);
            }
        }

        if (GameManager.instance.itemcheck[3])        // �ɹڱ��߰�
            quickSlots[3].gameObject.SetActive(true);

        if (GameManager.instance.itemcheck[4])       // ��ź��
        {
            quickSlots[4].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[4] == 3)
            {
                quickSlots[4].gameObject.SetActive(true);
                GameObject.Find("Armor").GetComponent<Image>().color = Color.white;
            }
            else if (GameManager.instance.itemcount[4] == 2)
                GameObject.Find("Armor").GetComponent<Image>().color = Color.yellow;
            else if (GameManager.instance.itemcount[4] == 1)
                GameObject.Find("Armor").GetComponent<Image>().color = Color.red;
            else if (GameManager.instance.itemcount[4] == 0)
                quickSlots[4].gameObject.SetActive(false);
        }
    }
}
