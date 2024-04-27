using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform target;
    Animator anim;
    string walk = "Walk";

    // Start is called before the first frame update
    void Start()
    {
        target = this.transform;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        nav.SetDestination(target.position);
        anim.SetFloat(walk, nav.velocity.magnitude);
    }
}
