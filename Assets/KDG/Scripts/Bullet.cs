using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int type;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    bool oneparticle = true;

    private void OnTriggerEnter(Collider other)
    {
        if(type == 0 && (other.CompareTag("Wall") || other.CompareTag("Enemy")))
            Destroy(gameObject);
        if(type == 1 && (other.CompareTag("Wall") || other.CompareTag("Player"))&& GameManager.instance.eonecollison)
        {
            GameManager.instance.eonecollison = false;
            GameObject explosionprefab =  Instantiate(explosion, gameObject.transform);
            Destroy(explosionprefab,0.5f);
            GameManager.instance.eonecollison = true;
            Destroy(gameObject,1f);
        }
        if(type == 3 && (other.CompareTag("Wall") || other.CompareTag("Player")))
            Destroy (gameObject);
    }
}
