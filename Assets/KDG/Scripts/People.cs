using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class People : MonoBehaviour
{
    Animator anim;
    GameManager gm;
    public UnityEvent peopleDieEvent;

    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameManager.instance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("E_Bullet"))
        {
            gm.peopledie = true;

            if(gm.peopledie)
            {

            }
        }
    }

    IEnumerator PeopleDie()
    {
        yield return new WaitForSeconds(2f);
    }
}
