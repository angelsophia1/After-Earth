using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnPointerEnter()
    {
        Weapon.canFire = false;
    }
    public void OnPointerExit()
    {
        if (Time.timeScale >0.1f)
        {
            Weapon.canFire = true;
        }
    }
}
