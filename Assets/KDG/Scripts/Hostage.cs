using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Hostage : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform target;
    Animator anim;
    public UnityEvent hostageDie;
    string walk = "Walk";
    string death = "Die";
    GameManager gm;

    public void Realive()
    {
        gm.hostagedie = false;
        nav.isStopped = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        target = this.transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.hostagedie)
        {
           transform.LookAt(target);
           nav.SetDestination(target.position);
           anim.SetFloat(walk, nav.velocity.magnitude);
        }
        else if(gm.hostagedie)
            return;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("E_Bullet"))
        {
            gm.hostagedie = true;
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            anim.SetTrigger(death);
            hostageDie.Invoke();
        }
    }
}
