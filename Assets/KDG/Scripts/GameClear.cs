using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using ItemInfo;

public class GameClear : MonoBehaviour
{
    public GameObject clear;
    public GameObject goal;
    public int value;
    public UnityEvent lastAction;
    public UnityEvent Ending;
    bool activewan;

    public List<GameObject> enemys;
    public GameObject items;
    CoolTime cool;

    private void Start()
    {
        activewan = true;
        cool = new CoolTime();
        if(value == 2)
        {
            StartCoroutine(EndingCheck());
        }
    }

    IEnumerator EndingCheck()
    {
        while (true)
        {
            foreach (GameObject enemy in enemys)
            {
                if (GameManager.instance.last && !enemy.activeSelf)
                {
                    enemys.Remove(enemy);
                    break;
                }

            }

            if(enemys.FirstOrDefault() == null && activewan)
            {
                goal.SetActive(true);
                items.SetActive(true);
                DataManager.instance.SaveData();
            }
                

            yield return cool.cool1sec;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && value == 1)
        {
            if (GameManager.instance.scenenum == 1)
            {
                clear.SetActive(true);
                SoundManager.instance.stage1Clear = true;
            }
                

            else if (GameManager.instance.scenenum == 3)
            {
                if (GameManager.instance.rescueHostage)
                {
                    clear.SetActive(true);
                    SoundManager.instance.stage2Clear = true;
                }
                else
                    return;
            }
        }
        else if(other.CompareTag("Player") && value == 2 && goal.activeSelf)
        {
            activewan = false;
            goal.SetActive(false);
            lastAction.Invoke();

            if (GameManager.instance.scenenum == 3 || GameManager.instance.scenenum == 4)
                EnemyLevel.enemylv.SetEnemy();
        }
        else if(other.CompareTag("Player") && value == 3)
        {
            if(GameManager.instance.scenenum == 2)
            {
                SoundManager.instance.stage1Clear = true;
                Ending.Invoke();
            }
            else if(GameManager.instance.scenenum == 4)
            {
                if (GameManager.instance.rescueHostage)
                {
                    SoundManager.instance.stage2Clear = true;
                    Ending.Invoke();
                }
                else
                    return;
            }
        }
    }

}
