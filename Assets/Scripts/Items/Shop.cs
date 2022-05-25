using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    public List<ItemData> shopItems;

    [SerializeField]
    public Inventory inventoryComponent;

    private bool refreshingPlayerInventory = false;
    private int retryTime = 1;

    public GameObject slotPrefab;
    public List<ShopSlot> shopSlots;

    void Start()
    {
        shopSlots = new List<ShopSlot>(shopItems.Count);
        DrawShop(shopItems);
    }

    void Update()
    {
        if(inventoryComponent == null && !refreshingPlayerInventory) StartCoroutine(RefreshPlayerInventory());
    }

    private void OnEnable()
    {
        Item.OnItemBought += inventoryComponent.Add;
    }

    private void OnDisable()
    {
        Item.OnItemBought -= inventoryComponent.Add;
    }

    private IEnumerator RefreshPlayerInventory()
    {
        inventoryComponent = GameManager.Instance.PlayerInventory;
        yield return new WaitForSeconds(retryTime);
    }

    void ResetShopUI()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        shopSlots = new List<ShopSlot>(shopItems.Count);
    }

    void DrawShop(List<ItemData> shop)
    {
        ResetShopUI();

        for (int i = 0; i < shopSlots.Capacity; i++)
        {
            CreateShopSlot();
        }

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopSlots[i].DrawSlot(shopItems[i]);
            // Debug.Log($"Drawn {shop[i].itemData.Name} in slot {i}.");
        }
    }

    void CreateShopSlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        ShopSlot newSlotComponent = newSlot.GetComponent<ShopSlot>();
        newSlotComponent.ClearSlot();

        shopSlots.Add(newSlotComponent);
    }
}
