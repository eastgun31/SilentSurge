using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursor;

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
        if (ui.isPauseWin || gm.nowpuzzle || ui.isGameOver || ui.isStageClear)
        {
            ResetCursor();
        }
        else if (!ui.isPauseWin || !gm.nowpuzzle || !ui.isGameOver || !ui.isStageClear)
        {
            CursorChange();
        }
    }
    void CursorChange()
    {
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }
     public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
