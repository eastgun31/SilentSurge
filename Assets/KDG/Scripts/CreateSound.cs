using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemInfo;

public class CreateSound : MonoBehaviour
{
    public GameObject soundprefab;
    public int value;
    Item delaytime = new Item();
    new SphereCollider collider;

    private void OnEnable()
    {
        if(value == 0)
        {
            StartCoroutine(SoundCreateDelete());
        }
    }

    IEnumerator SoundCreateDelete()
    {
        //yield return delaytime.colsize;
        GameObject sound = Instantiate(soundprefab);
        sound.transform.position = gameObject.transform.position;
        collider = sound.GetComponent<SphereCollider>();
        collider.radius = 0.1f;
        yield return delaytime.colsize;
        collider.radius = 0.5f;
        yield return delaytime.animDelay;
        collider.radius = 1f;
        yield return delaytime.animDelay;
        collider.radius = 1.5f;
        yield return delaytime.animDelay;
        collider.radius = 2f;
        yield return delaytime.animDelay;
        collider.radius = 2.5f;
        yield return delaytime.animDelay;
        collider.radius = 3f;

        Destroy(sound,1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(value == 1)
        {
            StartCoroutine (SoundCreateDelete());
        }
    }
}
