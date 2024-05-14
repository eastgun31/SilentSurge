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
    GameManager gm;

    private void Start()
    {
        activewan = true;
        cool = new CoolTime();
        if(value == 2)
        {
            StartCoroutine(EndingCheck());
        }
        gm = GameManager.instance;
    }

    IEnumerator EndingCheck()
    {
        while (true)
        {
            yield return cool.cool1sec;

            foreach (GameObject enemy in enemys)
            {
                if (gm.last && !enemy.activeSelf)
                {
                    enemys.Remove(enemy);
                    break;
                }

            }

            if(enemys.FirstOrDefault() == null && activewan)
            {
                goal.SetActive(true);
                items.SetActive(true);

                gm.clublast = true;

                DataManager.instance.SaveData();
            }
                

            //yield return cool.cool1sec;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && value == 1)
        {
            if (gm.scenenum == 1)
            {
                clear.SetActive(true);
                SoundManager.instance.stage1Clear = true;
            }
                

            else if (gm.scenenum == 3)
            {
                if (gm.rescueHostage)
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

            if (gm.scenenum == 3 || gm.scenenum == 4)
                EnemyLevel.enemylv.SetEnemy();

            if (gm.scenenum == 5)
                gm.people.SetActive(false);
        }
        else if(other.CompareTag("Player") && value == 3)
        {
            if(gm.scenenum == 2)
            {
                SoundManager.instance.stage1Clear = true;
                Ending.Invoke();
            }
            else if(gm.scenenum == 4)
            {
                if (gm.rescueHostage)
                {
                    SoundManager.instance.stage2Clear = true;
                    Ending.Invoke();
                }
                else
                    return;
            }
            else if(gm.scenenum == 5)
            {
                if(gm.clublast)
                    Ending.Invoke();
            }
        }
    }

}
