using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{
    public static event HandleItemCollected OnItemCollected;
    public delegate void HandleItemCollected(ItemData itemData);

    public static event HandleItemBought OnItemBought;
    public delegate void HandleItemBought(ItemData itemData);

    public ItemData specificItemData;

    public void Collect()
    {
        OnItemCollected?.Invoke(specificItemData);
        Destroy(gameObject);
    }

    public void Buy()
    {
        OnItemBought?.Invoke(specificItemData);
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = specificItemData.Icon;
    }
}
