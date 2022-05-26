using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
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
        // buyButton.onClick.AddListener(AttemptBuy);
        EnableAll();
    }

    void Update()
    {
        if (inventoryComponent == null && !refreshingPlayerInventory) StartCoroutine(RefreshPlayerInventory());
    }

    private void OnEnable()
    {
        ShopSlot.OnItemBought += inventoryComponent.Add;
    }

    private void OnDisable()
    {
        ShopSlot.OnItemBought -= inventoryComponent.Add;
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
            CreateShopSlot(i);
        }

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopSlots[i].DrawSlot(shopItems[i]);
            Debug.Log($"Drawn {shop[i].Name} in slot {i}.");
        }
    }

    void CreateShopSlot(int i)
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        ShopSlot newSlotComponent = newSlot.GetComponent<ShopSlot>();
        newSlotComponent.ClearSlot();

        shopSlots.Add(newSlotComponent);

        shopSlots[i].priceButton.onClick.AddListener(() => AttemptBuy(shopItems[i]));

    }

    private void AttemptBuy(ItemData item)
    {
        Debug.Log("Attempting Buy");
        if (CanAfford(item)) Buy(item);
    }

    private bool CanAfford(ItemData item)
    {
        int coins = GameManager.Instance.GetComponent<ScoreManager>().coins;
        Debug.Log($"{coins} Coins. Cost: {item.Price}. {coins - item.Price > 0}");
        if (coins - item.Price >= 0) return true;
        else return false;
    }

    private void Buy(ItemData item)
    {
        inventoryComponent.Add(item);
        GameManager.Instance.GetComponent<ScoreManager>().DecreaseCoins(item.Price);
        GameManager.Instance.GetComponent<UIController>().UpdateCoinValue();
    }

    public void EnableAll()
    {
        foreach (ShopSlot slot in shopSlots)
        {
            slot.GetComponent<ShopSlot>().enabled = true;
            slot.GetComponent<Image>().enabled = true;
        }
    }
}