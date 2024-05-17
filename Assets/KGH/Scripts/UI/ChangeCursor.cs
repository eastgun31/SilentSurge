using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    private Vector2 hotspot;

    GameManager gm;
    UiManager ui;

    private void Start()
    {
        gm = GameManager.instance;
        ui = UiManager.instance;

        if (cursor != null)
            hotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        else
            hotspot = Vector2.zero;
    }
    private void Update()
    {
        if (ui.isPauseWin || gm.nowpuzzle || ui.isGameOver)
        {
            ResetCursor();
        }
        else if (!ui.isPauseWin || !gm.nowpuzzle || !ui.isGameOver)
        {
            CursorChange();
        }
    }
    public void CursorChange()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }
     void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
