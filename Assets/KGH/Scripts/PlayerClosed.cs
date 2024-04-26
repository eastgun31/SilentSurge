using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClosed : MonoBehaviour
{
    public GameObject player;

    public GameObject clear;

    private void Start()
    {
        Time.timeScale = 1;
        player.SetActive(true);
        clear.SetActive(false);

    }
    public void PlayerClose()
    {
        player.SetActive(false);
    }

    public void Clear()
    {
        Time.timeScale = 0;
        clear.SetActive(true);
    }
}
