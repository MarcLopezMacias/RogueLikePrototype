using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public List<Item> Inventory;

    public void Add(Item newItem)
    {
        Inventory.Add(newItem);
    }

}
