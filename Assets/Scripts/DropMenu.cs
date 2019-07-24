using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMenu : MonoBehaviour
{
    public GameObject slot;
    public void Drop()
    {
        slot.GetComponent<Slot>().ClearSlot();
        Slot.dropMenuActive = false;
        Weapon.canFire = true;
        gameObject.SetActive(false);
    }
    public void Cancel()
    {
        Slot.dropMenuActive = false;
        Weapon.canFire = true;
        gameObject.SetActive(false);
    }
}
