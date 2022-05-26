using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    public Image icon;
    public Text nameText;
    public Button priceButton;
    public Text priceText;

    public bool selected;

    void Start()
    {
        priceButton.onClick.AddListener(Select);
    }

    public void ClearSlot()
    {
        icon.enabled = false;
        nameText.enabled = false;
        priceButton.enabled = false;
        priceText.enabled = false;
    }

    public void DrawSlot(ItemData item)
    {
        if (item == null)
        {
            Debug.Log($"{item.Name} was null");
            ClearSlot();
            return;
        }

        icon.enabled = true;
        nameText.enabled = true;
        priceButton.enabled = true;
        priceButton.enabled = false;

        icon.sprite = item.Icon;
        nameText.text = item.Name;
        priceText.text = item.Price.ToString();
    }

    private void Select()
    {
        selected = true;
    }

}
