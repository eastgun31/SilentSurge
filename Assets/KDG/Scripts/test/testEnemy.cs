using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testEnemy : MonoBehaviour
{
    Sight sight;
    NavMeshAgent nav;
    public Vector3 targetpos;
    public Transform targetdir;
    

    private void Start()
    {
        targetpos = targetdir.position;
        nav = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
    }

    private void Update()
    {
        if(sight.findT)
        {
            targetpos = sight.playerpos;
            nav.SetDestination(targetpos);
            Debug.Log("รณภ๛ม฿");
        }
        else
        {
            nav.isStopped = true;
            nav.velocity = Vector3.zero;
        }
    }



}
