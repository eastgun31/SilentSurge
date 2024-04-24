using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int type;
    [SerializeField]
    private GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if(type == 0)
            Destroy(gameObject);
        if(type == 1 && other.CompareTag("Player"))
        {
            GameObject explosionprefab =  Instantiate(explosion, gameObject.transform);
            Destroy(explosionprefab,0.5f);
            Destroy(gameObject,1f);
        }
    }
}
