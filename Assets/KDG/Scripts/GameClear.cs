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

    public List<GameObject> enemys;
    CoolTime cool;
    bool activeWan;

    private void Start()
    {
        activeWan = true;
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

            if(enemys.FirstOrDefault() == null && activeWan)
                goal.SetActive(true);

            yield return cool.cool1sec;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && value == 1)
        {
            clear.SetActive(true);
        }
        else if(other.CompareTag("Player") && value == 2 && goal.activeSelf)
        {
            activeWan = false;
            goal.SetActive(false);
            lastAction.Invoke();
        }
        else if(other.CompareTag("Player") && value == 3)
        {
            Ending.Invoke();
        }
    }

}
