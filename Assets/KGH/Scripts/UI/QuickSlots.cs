using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlots : MonoBehaviour
{
    public GameObject[] quickSlots;
    public GameObject[] selectedQuickSlots;
    public GameObject[] c_text;


    public Text[] i_count;

    Player playerInput;
    public GameObject player;

    UseItem useItem;

    private void Start()
    {
        playerInput = player.GetComponent<Player>();
        useItem = player.GetComponent<UseItem>();
    }
    private void Update()
    {
        AddSlots();
    }

    public void AddSlots()
    {
        if (GameManager.instance.itemcheck[0])       //±ÇÃÑ Ãß°¡
        {
            quickSlots[0].gameObject.SetActive(true);

            if (playerInput.handgunacivate == true)
            {
                selectedQuickSlots[0].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[0] >= 1)
                {
                    GameObject.Find("SelectedHandGun").GetComponent<Image>().color = Color.white;
                    i_count[0].text = GameManager.instance.itemcount[0].ToString();
                }
                else if (GameManager.instance.itemcount[0] == 0)
                {
                    GameObject.Find("SelectedHandGun").GetComponent<Image>().color = Color.gray;
                    c_text[0].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[0].gameObject.SetActive(false);
        }
        if (GameManager.instance.itemcheck[1])       //ÄÚÀÎ Ãß°¡
        {
            quickSlots[1].gameObject.SetActive(true);

            if(playerInput.coinacivate == true)
            {
                selectedQuickSlots[1].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[1] >= 1)
                {
                    GameObject.Find("SelectedCoin").GetComponent<Image>().color = Color.white;
                    i_count[1].text = GameManager.instance.itemcount[1].ToString();
                }
                else if (GameManager.instance.itemcount[1] == 0)
                {
                    GameObject.Find("SelectedCoin").GetComponent<Image>().color = Color.gray;
                    c_text[1].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[1].gameObject.SetActive(false);
        }

        if (GameManager.instance.itemcheck[2])      // ¼¶±¤ÅºÃß°¡
        {
            quickSlots[2].gameObject.SetActive(true);

            if (playerInput.flashbangacivate == true)
            {
                selectedQuickSlots[2].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[2] >= 1)
                {
                    GameObject.Find("SelectedFlashbang").GetComponent<Image>().color = Color.white;
                    i_count[2].text = GameManager.instance.itemcount[2].ToString();
                }
                else if (GameManager.instance.itemcount[2] == 0)
                {
                    GameObject.Find("SelectedFlashbang").GetComponent<Image>().color = Color.gray;
                    c_text[2].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[2].gameObject.SetActive(false);
        }

        if (GameManager.instance.itemcheck[3])  // ½É¹Ú±âÃß°¡
        {
            quickSlots[3].gameObject.SetActive(true);

            if (playerInput.heartseeacivate == true)
            {
                selectedQuickSlots[3].SetActive(true);
                if(useItem.heartCanUse==true)
                    GameObject.Find("SelectedHeart").GetComponent<Image>().color = Color.white;
                else
                    GameObject.Find("SelectedHeart").GetComponent<Image>().color = Color.gray;
            }
            else
                selectedQuickSlots[3].SetActive(false);
        }

        if (GameManager.instance.itemcheck[4])       // ¹æÅºº¹
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
        else
        {
            quickSlots[4].gameObject.SetActive(false);
        }
    }
}
