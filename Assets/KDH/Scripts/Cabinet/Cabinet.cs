using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour
{
    public Player playerState;

    void Update()
    {
        HideOnCabinet();
    }

    void HideOnCabinet()
    {
        if (playerState.state == Player.PlayerState.idle)
            playerState.state = Player.PlayerState.hide;
        else
            playerState.state = Player.PlayerState.idle;
    }
}
