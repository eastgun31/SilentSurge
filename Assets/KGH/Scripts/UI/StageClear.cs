using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{

    private void Start()
    {
        UiManager.instance.isStageClear = true;
    }
    public void GoTomain()
    {
        SceneManager.LoadScene("GameStart");
    }
}
