using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public bool directed;
    public CursorManager cursorManager;
    private bool isMouseOver;
    private void Start()
    {
        isMouseOver = false;
    }
    private void OnMouseEnter()
    {
        cursorManager.MouseEnterItem();
    }
    private void OnMouseOver()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        cursorManager.MouseExit();
        isMouseOver = false;
    }
    private void OnDestroy()
    {
        if (isMouseOver)
        {
            cursorManager.MouseExit();
        }
    }
}
