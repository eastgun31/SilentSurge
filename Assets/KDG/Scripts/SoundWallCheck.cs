using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWallCheck : MonoBehaviour
{
    string enemy ;
    LayerMask mask;
    RaycastHit hit;
    
    private void Start()
    {
        enemy = "RayDir";
        mask = LayerMask.GetMask("Enemy") | LayerMask.GetMask("Wall");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag(enemy))
    //    {
    //        ContactPoint contact = collision.contacts[0];

    //        Debug.DrawRay(transform.position, contact.point);
    //        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), contact.point, out hit, 3f ,mask))
    //        {
    //            Debug.Log(hit.collider.gameObject.layer);
    //            if (hit.collider.gameObject.layer == 9)
    //            {
    //                Debug.Log("���� ����");
    //            }
    //            else if (hit.collider.gameObject.layer == 10)
    //            {
    //                Debug.Log("���� �Ҹ� ����");
    //            }
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(enemy))
        {
            Debug.Log("�浹");
            hit = new RaycastHit();
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), other.transform.position);
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), other.transform.position, out hit,mask))
            {
                Debug.Log(hit.collider.gameObject.layer);
                if (hit.collider.gameObject.layer == 9)
                {
                    Debug.Log("���� ����");
                }
                else if (hit.collider.gameObject.layer == 10)
                {
                    Debug.Log("���� �Ҹ� ����");
                }
            }
        }
    }
}
