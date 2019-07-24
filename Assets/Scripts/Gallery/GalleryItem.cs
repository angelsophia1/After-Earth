using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GalleryItem
{
    public Sprite itemSprite;
    public string itemName;
    public int id;

    [TextArea(6,1)]
    public string itemDescription;
}
