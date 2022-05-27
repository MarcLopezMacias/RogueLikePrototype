using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> fancyInventory = new List<InventoryItem>();
    protected Dictionary<ItemData, InventoryItem> fancyDictionary = new Dictionary<ItemData, InventoryItem>();

    private List<WeaponScriptableObject> weapons = new List<WeaponScriptableObject>();

    private int currentSlot = 1;
    private Weapon weaponComponent;

    private int timeInterval = 1;

    void Update()
    {
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            if (CanSwitchWeapon())
            {
                SwitchWeapon();
            }
        }

        if(Input.GetButtonDown("Heal"))
        {
            AttemptHeal();
            AttemptUse1UP();
        }

        if (Input.GetButtonDown("ApplyBuffs"))
        {
            AttemptApplyBuffs();
        }

        if (weaponComponent == null)
        {
            weaponComponent = GameManager.Instance.PlayerWeaponComponent;
        }
    }

    private void OnEnable()
    {
        Item.OnItemCollected += Add;
        ShopSlot.OnItemBought += Add;
    }

    private void OnDisable()
    {
        Item.OnItemCollected -= Add;
        ShopSlot.OnItemBought -= Add;
    }

    public void Add(ItemData itemData)
    {
            // ALREADY IN INVENTORY
        if(fancyDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            // IF ITS A WEAPON or non-stackable?
            if (itemData.Type.ToString() == "Active")
            {
                ActiveItem activeItem = (ActiveItem)itemData;
                if (activeItem.activeType.ToString() == "Weapon")
                {
                    WeaponScriptableObject weapon = (WeaponScriptableObject)activeItem;
                    weapon.FullReload();
                }
            }
            else
            {
                item.AddToStack();
                Debug.Log($"{item.itemData.Name} stack size is now {item.stackSize}");
                OnInventoryChange?.Invoke(fancyInventory);
            }

        }
            // FIRST TIME
        else
        {
                // ADD IT TO INVENTORY
            InventoryItem newItem = new InventoryItem(itemData);
            fancyInventory.Add(newItem);
            fancyDictionary.Add(itemData, newItem);

            // IF ITS A WEAPON or non-stackable?
            if (itemData.Type.ToString() == "Active")
            {
                ActiveItem activeItem = (ActiveItem)itemData;
                if (activeItem.activeType.ToString() == "Weapon")
                {
                    WeaponScriptableObject weapon = (WeaponScriptableObject)activeItem;
                    UpdateWeapon(weapon);
                    currentSlot = fancyInventory.Count;
                    weapons.Add(weapon);
                }
            }

            Debug.Log($"Added {newItem.itemData.Name} for the first time.");
            OnInventoryChange?.Invoke(fancyInventory);
        }
    }

    public void Remove(ItemData itemData)
    {
        if(fancyDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                fancyInventory.Remove(item);
                fancyDictionary.Remove(itemData);
                if (itemData.Type.ToString() == "Active")
                {
                    ActiveItem activeItem = (ActiveItem)itemData;
                    if (activeItem.activeType.ToString() == "Weapon")
                    {
                        WeaponScriptableObject weapon = (WeaponScriptableObject)activeItem;
                        weapons.Remove(weapon);
                    }
                }
            }
            else
            {
                Debug.Log("Item used. Not deleted because still have more left.");
            }
            OnInventoryChange?.Invoke(fancyInventory);
        }
    }

    private bool CanSwitchWeapon()
    {
        return weapons.Count > 1;
    }

    private void SwitchWeapon()
    {
        if (currentSlot == weapons.Count)
        {
            currentSlot = 0;
        }
        else currentSlot += 1;

        weaponComponent.weaponData = weapons[currentSlot];
        weaponComponent.GetComponent<SpriteRenderer>().sprite = weapons[currentSlot].Icon;
        
        Debug.Log($"Switched to slot {currentSlot}");
    }

    public void UpdateWeapon(WeaponScriptableObject newWeaponData)
    {
        if (weaponComponent == null)
        {
            Debug.Log("Failed at Updating Weapon. Weapon Component NOT FOUND. Retrying soon.");
            StartCoroutine(RetrySoon(newWeaponData, timeInterval));
        }
        else
        {
            weaponComponent.weaponData = newWeaponData;
            weaponComponent.GetComponent<SpriteRenderer>().sprite = newWeaponData.Icon;
            weaponComponent.weaponData.FullReload();
        }
    }

    private IEnumerator RetrySoon(WeaponScriptableObject weaponData, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateWeapon(weaponData);
    }

    private void AttemptHeal()
    {
        Consumables HealthPot = FindThePot();
        if(HealthPot != null)
        {
            GameManager.Instance.Player.GetComponent<Player>().Heal(HealthPot.AmountToConsume);
            Remove(HealthPot);
        }
    }

    private Consumables FindThePot()
    {
        foreach (InventoryItem item in fancyInventory)
        {
            if (item.itemData.Name.ToString() == "Restore HP")
            {
                return (Consumables)item.itemData;
            }
        }
        return null;
    }

    private void AttemptUse1UP()
    {
        Consumables ExtraLife = FindTheExtraLife();
        if (ExtraLife != null)
        {
            GameManager.Instance.Player.GetComponent<Player>().IncreaseLifes(ExtraLife.AmountToConsume);
            Remove(ExtraLife);
        }
    }

    private Consumables FindTheExtraLife()
    {
        foreach (InventoryItem item in fancyInventory)
        {
            if (item.itemData.Name.ToString() == "1 UP")
            {
                return (Consumables)item.itemData;
            }
        }
        return null;
    }

    private void AttemptApplyBuffs()
    {
        List<Buffs> buffs = FindBuffs();
        if (buffs.Count > 0)
        {
            foreach(Buffs buff in buffs)
            {
                GameManager.Instance.Player.GetComponent<Player>().ApplyBuff(buff);
                Remove(buff);
            }
        }
    }

    private List<Buffs> FindBuffs()
    {
        List<Buffs> list = new List<Buffs>();
        foreach (InventoryItem item in fancyInventory)
        {
            if (item.itemData.Type.ToString() == "Passive")
            {
                list.Add((Buffs)item.itemData);
            }
        }
        return list;
    }

    public List<ItemData> GetInventoryItems()
    {
        List<ItemData> items = new List<ItemData>();
        foreach (InventoryItem item in fancyInventory)
        {
            items.Add(item.itemData);
        }
        return items;
    }

    public void ResetInventory()
    {
        fancyInventory = new List<InventoryItem>();
        fancyDictionary = new Dictionary<ItemData, InventoryItem>();
    }
}
