using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sight : MonoBehaviour
{
    public float radius = 30f;
    [Range(0,360)]
    public float angle;

    public LayerMask playerM, objectM;
    
    public List<Transform> detectT = new List<Transform>();



    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    IEnumerator DetectDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            DetectTargets();
        }
    }

    void DetectTargets()
    {
        detectT.Clear();
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, playerM);
        for (int i = 0; i < targets.Length; i++)
        {
            Transform detectTarget = targets[i].transform;
            Vector3 dirT = (detectTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirT) < angle / 2)
            {
                float disT = Vector3.Distance(transform.position, detectTarget.position);
                if(!Physics.Raycast(transform.position, dirT, disT, objectM))
                {
                    detectT.Add(detectTarget);
                }
            }
        }
    }
} //https://nicotina04.tistory.com/197
