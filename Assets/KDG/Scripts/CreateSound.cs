using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSound : MonoBehaviour
{
    public GameObject soundprefab;
    public int value;

    //SphereCollider collider;

    private void OnEnable()
    {
        if(value == 0)
        {
            StartCoroutine(SoundCreateDelete());
        }
    }

    IEnumerator SoundCreateDelete()
    {
        GameObject sound = Instantiate(soundprefab);
        sound.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(1f);
        Destroy(sound,3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(value == 1)
        {
            StartCoroutine (SoundCreateDelete());
        }
    }
}
