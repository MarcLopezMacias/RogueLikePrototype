using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    public Image icon;
    public Text labelText;

    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
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
        labelText.enabled = true;

        icon.sprite = item.Icon;
        labelText.text = item.Name;
    }
}
