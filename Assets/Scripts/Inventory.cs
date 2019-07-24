using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
    public GameObject dropMenu;
    public AudioSource buttonClickedSource;
    public int[] taskNumbers;
    public int[] taskItemID;
    [SerializeField]
    private int allslots;
    private GameObject[] slot;
    private Vector3 dropMenuPositionToShow;
    // Start is called before the first frame update
    void Start()
    {
        slot = new GameObject[allslots];
        for (int i = 0; i < allslots; i++)
        {
            slot[i] = transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().slotItem == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
        dropMenuPositionToShow = dropMenu.transform.position;
    }
    public bool AddItem(int id,string itemName, Sprite itemSprite)
    {
        for (int i = 0; i < allslots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                slot[i].GetComponent<Slot>().id = id;
                slot[i].GetComponent<Slot>().itemName = itemName;
                slot[i].GetComponent<Slot>().itemSprite = itemSprite;

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return true;
            }else if (slot[i].GetComponent<Slot>().id == id)
            {
                slot[i].GetComponent<Slot>().UpdateSlot();
                FindObjectOfType<GameUIManager>().CheckIfGameClear();
                return true;
            }
        }
        return false;
    }
    public void DisplayDropMenu(float xPositionToShow, GameObject slot)
    {
        buttonClickedSource.Play();
        dropMenuPositionToShow.x = xPositionToShow;
        dropMenu.GetComponent<RectTransform>().position = dropMenuPositionToShow;
        dropMenu.GetComponent<DropMenu>().slot = slot ;
        dropMenu.SetActive(true);
    }
    public bool CheckIfGameClear()
    {
        bool[] isCleared = new bool[taskItemID.Length];
        for (int i = 0; i < taskItemID.Length; i++)
        {
            for (int j = 0; j<allslots;j++ )
            {
                if (slot[j].GetComponent<Slot>().id == taskItemID[i])
                {
                    if (slot[j].GetComponent<Slot>().numberOfCollected < taskNumbers[i])
                    {
                        return false;
                    }
                    else
                    {
                        isCleared[i] = true;
                    }
                }
                if (isCleared[i])
                {
                    break;
                }
            }
            if (!isCleared[i])
            {
                return false;
            }
        }
        return true;
    }
}
