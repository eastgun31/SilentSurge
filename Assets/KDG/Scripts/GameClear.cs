using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using ItemInfo;

public class GameClear : MonoBehaviour
{
    public GameObject clear;
    public int value;
    public UnityEvent lastAction;

    public List<GameObject> enemys;
    CoolTime cool;

    private void Start()
    {
        cool = new CoolTime();
    }

    IEnumerator EndingCheck()
    {
        while (true)
        {
            foreach (GameObject enemy in enemys)
            {
                if (!enemy.activeSelf)
                    enemys.Remove(enemy);
            }
            

            yield return cool.cool1sec;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && value == 1)
            clear.SetActive(true);
        else if(other.CompareTag("Player") && value == 2)
        {
            GameManager.instance.enemyDown = true;
            lastAction.Invoke();
        }
    }

}
