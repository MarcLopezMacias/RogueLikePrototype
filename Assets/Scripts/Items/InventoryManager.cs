using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public static int InventorySize = 5;

    [SerializeField]
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(InventorySize);

    private void OnEnable()
    {
        Inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= DrawInventory;
    }

    void ResetInventory()
    {
        foreach(Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }

        inventorySlots = new List<InventorySlot>(InventorySize);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for(int i = 0; i <  inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for(int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventory[i]);
            // Debug.Log($"Drawn {inventory[i].itemData.Name} in slot {i}.");
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }
}
