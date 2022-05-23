using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    /** Constructor for InventoryItem class. */
    public InventoryItem(ItemData item)
    {
        this.itemData = item;
        AddToStack();
    }

    /** Increase stack size of item. */
    public void AddToStack()
    {
        this.stackSize++;
    }

    /** Decrease stack size of item. */
    public void RemoveFromStack()
    {
        this.stackSize--;
    }
}