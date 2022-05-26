using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{

    [SerializeField]
    public Image icon;

    [SerializeField]
    public Image nameLabel;

    [SerializeField]
    public Text nameText;

    [SerializeField]
    public Button priceButton;

    [SerializeField]
    public Text priceText;

    public bool selected;

    void Start()
    {

    }

    public void ClearSlot()
    {
        icon.enabled = false;
        nameLabel.enabled = false;
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
        nameLabel.enabled = true;
        nameText.enabled = true;
        priceButton.enabled = true;
        priceText.enabled = true;

        icon.sprite = item.Icon;
        nameText.text = item.Name;
        priceText.text = item.Price.ToString();
    }

    private void OnEnable()
    {
        Start();
    }

}
