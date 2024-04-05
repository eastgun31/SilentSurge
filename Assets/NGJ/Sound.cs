using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sound")
        {
            Debug.Log("¹ß¼Ò¸®");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
