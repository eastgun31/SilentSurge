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
        if(other.CompareTag("Bullet"))
        {

            if(!gm.peopledie)
                StartCoroutine(PeopleDie());

            peopleDieEvent.Invoke();
            
        }
    }

    IEnumerator PeopleDie()
    {
        gm.peopledie = true;
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(2f);
        //anim.SetBool("Die", false);
        gm.peopledie = false;
    }
}
