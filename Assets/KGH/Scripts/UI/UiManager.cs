using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager Ui_instance;

    public static UiManager instance {  get { return Ui_instance; } }

    public GameObject pauseScreen;
    public GameObject sinPuzzle1;


    public void Awake()
    {

        if (Ui_instance != null)
            Destroy(gameObject);
        else
            Ui_instance = this;
    }

}
