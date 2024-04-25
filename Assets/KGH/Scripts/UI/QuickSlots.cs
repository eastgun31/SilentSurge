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

    public Image[] i_color;
    public Image boundary;

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
        if (GameManager.instance.itemcheck[0])       //권총 추가
        {
            quickSlots[0].gameObject.SetActive(true);

            if (playerInput.handgunacivate == true)
            {
                selectedQuickSlots[0].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[0] >= 1)
                {
                    i_color[0].color = Color.white;
                    i_count[0].text = GameManager.instance.itemcount[0].ToString();
                }
                else if (GameManager.instance.itemcount[0] == 0)
                {
                    i_color[0].color = Color.gray;
                    c_text[0].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[0].gameObject.SetActive(false);
        }
        else
            quickSlots[0].gameObject.SetActive(false);


        if (GameManager.instance.itemcheck[1])       //코인 추가
        {
            quickSlots[1].gameObject.SetActive(true);

            if(playerInput.coinacivate == true)
            {
                selectedQuickSlots[1].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[1] >= 1)
                {
                    i_color[1].color = Color.white;
                    i_count[1].text = GameManager.instance.itemcount[1].ToString();
                }
                else if (GameManager.instance.itemcount[1] == 0)
                {
                    i_color[1].color = Color.gray;
                    c_text[1].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[1].gameObject.SetActive(false);
        }
        else
            quickSlots[1].gameObject.SetActive(false);

        if (GameManager.instance.itemcheck[2])      // 섬광탄추가
        {
            quickSlots[2].gameObject.SetActive(true);

            if (playerInput.flashbangacivate == true)
            {
                selectedQuickSlots[2].gameObject.SetActive(true);

                if (GameManager.instance.itemcount[2] >= 1)
                {
                    i_color[2].color = Color.white;
                    i_count[2].text = GameManager.instance.itemcount[2].ToString();
                }
                else if (GameManager.instance.itemcount[2] == 0)
                {
                    i_color[2].color = Color.gray;
                    c_text[2].gameObject.SetActive(false);
                }
            }
            else
                selectedQuickSlots[2].gameObject.SetActive(false);
        }
        else
            quickSlots[2].gameObject.SetActive(false);

        if (GameManager.instance.itemcheck[3])  // 심박기추가
        {
            quickSlots[3].gameObject.SetActive(true);

            if (playerInput.heartseeacivate == true)
            {
                selectedQuickSlots[3].SetActive(true);
                if(useItem.heartCanUse==true)
                    i_color[3].color = Color.white;
                else
                    i_color[3].color = Color.gray;
            }
            else
                selectedQuickSlots[3].SetActive(false);
        }
        else
            quickSlots[3].gameObject.SetActive(false);

        if (GameManager.instance.itemcheck[4])       // 방탄복
        {
            quickSlots[4].gameObject.SetActive(true);
            if (GameManager.instance.itemcount[4] == 3)
            {
                quickSlots[4].gameObject.SetActive(true);
                i_color[4].color = Color.white;
            }
            else if (GameManager.instance.itemcount[4] == 2)
                i_color[4].color = Color.yellow;
            else if (GameManager.instance.itemcount[4] == 1)
                i_color[4].color = Color.red;
            else if (GameManager.instance.itemcount[4] == 0)
                quickSlots[4].gameObject.SetActive(false);
        }
        else
            quickSlots[4].gameObject.SetActive(false);
        
        //---------------경계단계 별 색 변경-------
        if(EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level1)
            boundary.color = Color.green;
        if(EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level2)
            boundary.color = Color.yellow;
        if(EnemyLevel.enemylv.LvStep == EnemyLevel.ELevel.level3)
            boundary.color = Color.red;
        
    }
}
