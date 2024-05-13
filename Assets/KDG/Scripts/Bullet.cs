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

    private string wall = "Wall";
    private string player = "Player";
    private string cabinet = "CabinetObj";
    private string people = "People";

    private void Start()
    {
        if(type == 4)
            Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(type == 0 && (other.CompareTag(wall) || other.CompareTag(people)))
            Destroy(gameObject);
        if(type == 1 && (other.CompareTag(wall) || other.CompareTag(player))&& GameManager.instance.eonecollison)
        {
            GameManager.instance.eonecollison = false;
            GameObject explosionprefab =  Instantiate(explosion, gameObject.transform);
            Destroy(explosionprefab,0.5f);
            Destroy(gameObject,1f);
        }
        if(type == 3 && (other.CompareTag(wall) || other.CompareTag(player) || other.CompareTag(cabinet) ))
            Destroy (gameObject);

    }
}
