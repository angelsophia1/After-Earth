using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour
{
    public int id, numberOfCollected;
    public string itemName;
    public Sprite itemSprite;
    public GameObject slotItem;
    public TextMeshProUGUI numberText;
    public static bool dropMenuActive;
    public bool empty;
    private Sprite background;
    private void Start()
    {
        id = 0;
        empty = true;
        background = slotItem.GetComponent<Image>().sprite;
    }
    public void OnPointerEnter()
    {
        Weapon.canFire = false;
    }
    public void OnPointerExit()
    {
        if (!dropMenuActive)
        {
            Weapon.canFire = true;
        }
    }
    public void OnPointerClick()
    {
        if (!empty)
        {
            FindObjectOfType<Inventory>().DisplayDropMenu(transform.position.x,gameObject);
            dropMenuActive = true;
        }
    }
    public void UpdateSlot()
    {
        if (empty)
        {
            slotItem.SetActive(true);
            slotItem.GetComponent<Image>().sprite = itemSprite;
        }
        numberOfCollected++;
        numberText.text = numberOfCollected.ToString();
    }
    public void ClearSlot()
    {
        if (!empty)
        {
            id = 0;
            itemName = null;
            itemSprite = background;
            numberOfCollected = 0;
            numberText.text = numberOfCollected.ToString();
            slotItem.GetComponent<Image>().sprite = itemSprite;
            empty = true;
            slotItem.SetActive(false);
        }
    }
}
