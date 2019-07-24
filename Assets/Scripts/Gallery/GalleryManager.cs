using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryManager : MonoBehaviour
{
    public GalleryItem[] items;

    public Image slotImage;
    public TextMeshProUGUI displayNameText, displayDescriptionText,currentPageText, MaxPage;

    [SerializeField] private int currentNumber;


    private void Awake()
    {
        currentNumber = 0;
    }

    void Update()
    {
        ShowInfo();
        ShowPage();
    }

    private void ShowInfo()
    {
        slotImage.sprite = items[currentNumber].itemSprite;
        displayNameText.text = items[currentNumber].itemName;
        displayDescriptionText.text = items[currentNumber].itemDescription;
    }

    public void Backbutton()
    {
        currentNumber--;
        if(currentNumber < 0)
        {
            currentNumber = items.Length - 1;
        }
    }

    public void NextButton()
    {
        currentNumber++;
        if(currentNumber >= items.Length)
        {
            currentNumber = 0;
        }
    }

    private void ShowPage()
    {
        currentPageText.text = (currentNumber + 1).ToString();
        MaxPage.text = items.Length.ToString();
    }

}
