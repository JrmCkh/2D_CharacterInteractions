using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /** Event is fired whenever the inventory updates (add/remove/etc). */
    public static event Action<List<InventoryItem>> OnInventoryChange;

    /** List of inventory items. */
    private List<InventoryItem> inventory = new List<InventoryItem>();

    /**
     * Dictionary is used for the stacking of item.
     * Key: ItemData (image, name, and text of item).
     * Value: InventoryItem (Add/Remove from stack).
     */
    private Dictionary<ItemData, InventoryItem> itemDictionary 
        = new Dictionary<ItemData, InventoryItem>();

    /** 
     * OnEnable and OnDisable is used for garbage collection purposes due to being an static event. 
     * Every collectible items is to be added.
     */
    private void OnEnable()
    {
        CollectCoin.OnCoinCollected += Add;
        CollectCrystal.OnCrystalCollected += Add;
        CollectConsumable.OnConsumableCollected += Add;
    }

    private void OnDisable()
    {
        CollectCoin.OnCoinCollected -= Add;
        CollectCrystal.OnCrystalCollected -= Add;
        CollectConsumable.OnConsumableCollected -= Add;
    }

    /** Add method for adding item into Inventory. */
    public void Add(ItemData itemData)
    {
        // Check if item exists in dictionary (via key).
        // If exists, item is returned and stack is increased;
        // otherwise, item returns null.
        if (this.itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.itemName} total stack is now {item.stackSize}");
            OnInventoryChange?.Invoke(this.inventory);
        }
        else
        {
            // If item do not exists, item is added to the inventory and dictionary for the first time.
            InventoryItem newItem = new InventoryItem(itemData);
            this.inventory.Add(newItem);
            this.itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.itemName} to the inventory for the first time");
            OnInventoryChange?.Invoke(this.inventory);
        }
    }

    /** Remove method for removing item from inventory */
    public void Remove(ItemData itemData)
    {
        if (this.itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            // If item stack reaches 0, item is removed from inventory and dictionary.
            if (item.stackSize == 0)
            {
                this.inventory.Remove(item);
                this.itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(this.inventory);
        }
    }

    /** Modify method to change the index of the items in the inventory (when swapped). */
    public void Modify(int prevPos, int newPos)
    {
        InventoryItem prevItem = this.inventory[prevPos];
        this.inventory[prevPos] = this.inventory[newPos];
        this.inventory[newPos] = prevItem;
    }
}