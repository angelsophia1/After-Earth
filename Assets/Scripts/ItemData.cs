using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Item",menuName = "Items")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemSprite;
}
