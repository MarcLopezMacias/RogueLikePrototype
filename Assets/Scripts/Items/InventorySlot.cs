using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Text labelText;
    public Text stackSizeText;

    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item.itemData == null)
        {
            Debug.Log($"{item.itemData.Name} was null");
            ClearSlot();
            return;
        }

        icon.enabled = true;
        labelText.enabled = true;
        stackSizeText.enabled = true;

        icon.sprite = item.itemData.Icon;
        labelText.text = item.itemData.Name;
        stackSizeText.text = item.stackSize.ToString();
    }
}
