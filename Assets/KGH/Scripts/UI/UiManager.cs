using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Ui_instance;


    public void Awake()
    {

        if (Ui_instance != null)
            Destroy(gameObject);
        else
            Ui_instance = this;
    }
}
