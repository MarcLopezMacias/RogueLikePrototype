using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{
    public static event HandleItemCollected OnItemCollected;
    public delegate void HandleItemCollected(ItemData itemData);

    public ItemData specificItemData;

    public void Collect()
    {
        OnItemCollected?.Invoke(specificItemData);
        Destroy(gameObject);
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = specificItemData.Icon;
    }
}
