using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerClosed : MonoBehaviour
{
    public GameObject player;

    public GameObject clear;

    public PlayableDirector ending;

    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        ending.Play();
        SoundManager.instance.EffectPlay(2, true, 1f);
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
        clear.SetActive(true);
        ending.Stop();
    }
}
