using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cursor Manager", menuName = "Cursor Manager")]
public class CursorManager : ScriptableObject
{
    public Texture2D highlightedCursor, normalCursor,enemyDetectCursor,searchCursor;
    public static CursorManager instance;

    public void MouseEnter()
    {
        Cursor.SetCursor(highlightedCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void MouseExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void MouseEnterEnemy()
    {
        Cursor.SetCursor(enemyDetectCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void MouseEnterItem()
    {
        Cursor.SetCursor(searchCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
