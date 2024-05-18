using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Stage3TimeLine : MonoBehaviour
{
    public PlayableDirector playable;

    public GameObject walk_p;
    public GameObject idle_p;
    public GameObject clear;
    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void SetIdle()
    {
        walk_p.SetActive(false);    
        idle_p.SetActive(true);
    }

    public void StageClear()
    {
        clear.SetActive(true);
        playable.Stop();
    }
}
