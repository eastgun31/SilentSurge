using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWallCheck : MonoBehaviour
{
    LayerMask mask;
    RaycastHit hit;
    WaitForSeconds wait;

    public bool canhear;
    
    private void Start()
    {
        mask = LayerMask.GetMask("Floor") | LayerMask.GetMask("InRoom");
        wait = new WaitForSeconds(0.5f);
    }

    private void OnEnable()
    {
        StartCoroutine(SoundPosCheck());
    }

    IEnumerator SoundPosCheck()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), Vector3.down, out hit, 2f, mask))
        {
            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == 6)
            {
                //Debug.Log("적이 들음");
                canhear = true;
            }
            else if (hit.collider.gameObject.layer == 9)
            {
                //Debug.Log("벽에 소리 막힘");
                canhear = false;
            }
        }

        yield return wait;
        StartCoroutine(SoundPosCheck());
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(enemy))
    //    {
    //        Debug.Log("충돌");
    //        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), Vector3.down);
    //        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), Vector3.down, out hit,2f,mask))
    //        {
    //            Debug.Log(hit.collider.gameObject.layer);
    //            if (hit.collider.gameObject.layer == 6)
    //            {
    //                Debug.Log("적이 들음");
    //                canhear = true;
    //            }
    //            else if (hit.collider.gameObject.layer == 9)
    //            {
    //                Debug.Log("벽에 소리 막힘");
    //                canhear= false;
    //            }
    //        }
    //    }
    //}
}
