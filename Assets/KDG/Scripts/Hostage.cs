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
    bool die;



    // Start is called before the first frame update
    void Start()
    {
        die = false;
        target = this.transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!die)
        {
           transform.LookAt(target);
           nav.SetDestination(target.position);
           anim.SetFloat(walk, nav.velocity.magnitude);
        }
        else if(die)
            return;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("E_Bullet"))
        {
            die = true;
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
            anim.SetTrigger(death);
            hostageDie.Invoke();
            die = false;
        }
    }
}
