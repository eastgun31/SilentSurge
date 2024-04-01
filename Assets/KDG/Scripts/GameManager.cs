using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool[] itemcheck = new bool[5];


    public void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

}
