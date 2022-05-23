using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // [SerializeField]
    // private Inventory inventory;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private InventoryDescription itemDescription;
    [SerializeField]
    private MouseFollower mouseFollower;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private int currentlyDraggedItemIndex = -1;

    /** OnEnable/OnDisable is for garbage collection purposes due to being an static event. */
    private void OnEnable()
    {
        Inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChange -= DrawInventory;
    }

    private void Awake()
    {
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    /** Clear inventory, recreate and update the slots every time it updates (add/remove/etc). */
    private void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            CreateInventorySlot();
        }
        for (int i = 0; i < inventory.Count; i++)
        {
            this.inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    /** Reset inventory (destroy all gameObject). */
    private void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        this.inventorySlots = new List<InventorySlot>();
    }

    /** Create an inventory slot. */
    private void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponenet = newSlot.GetComponent<InventorySlot>();
        newSlotComponenet.ClearSlot();

        newSlotComponenet.OnItemClicked += HandleItemSelection;
        newSlotComponenet.OnItemBeginDrag += HandleBeginDrag;
        newSlotComponenet.OnItemDroppedOn += HandleSwap;
        newSlotComponenet.OnItemEndDrag += HandleEndDrag;
        newSlotComponenet.OnRightMouseBtnClick += HandleShowItemActions;

        this.inventorySlots.Add(newSlotComponenet);
    }

    /** Run whenever inventory is opened, all items are deselected and description is resetted. */
    public void OpenInventory()
    {
        DeselectAllItem();
        this.itemDescription.ResetDescription();
    }

    /** Deselect all items in the inventory slots. */
    private void DeselectAllItem()
    {
        foreach (InventorySlot item in this.inventorySlots)
        {
            item.Deselect();
        }
    }

    /** Run when a item is clicked. */
    private void HandleItemSelection(InventorySlot slot)
    {
        if (slot.IsSelected())
        {
            DeselectAllItem();
            this.itemDescription.ResetDescription();
        }
        else
        {
            DeselectAllItem();
            slot.Select();
            this.itemDescription.SetDescription(slot.GetImage(), slot.GetName(), slot.GetDescription());
        }
    }

    /** 
     * Run at the start of a item drag operation.
     * Draws the mouse follower with the dragged slot.
     */
    private void HandleBeginDrag(InventorySlot slot)
    {
        // Get the index of the dragged inventory slot.
        int index = this.inventorySlots.IndexOf(slot);
        if (index == -1)
            return;
        this.currentlyDraggedItemIndex = index;

        // Deselect any inventory slot if selected.
        DeselectAllItem();

        // Draw the mouse follower.
        this.mouseFollower.Toggle(true);
        this.mouseFollower.SetData(slot.GetItem());
    }

    /** Run at the end of a item drag operation. */
    private void HandleEndDrag(InventorySlot slot)
    {
        mouseFollower.Toggle(false);
    }

    /** 
     * Run when two items are swapped.
     * If new slot is empty, clear the old (dragged) slot and draw the new slot;
     * otherwise, redraw the old (dragged) slot and redraw the new slot.
     */
    private void HandleSwap(InventorySlot slot)
    {
        int index = inventorySlots.IndexOf(slot);
        if (index == -1)
        {
            this.mouseFollower.Toggle(false);
            this.currentlyDraggedItemIndex = -1;
            return;
        }

        // Item of the old (dragged) slot.
        InventoryItem inventoryItem = this.inventorySlots[this.currentlyDraggedItemIndex].GetItem();
        if (slot.IsEmpty())
        {
            // Clear the old slot.
            this.inventorySlots[this.currentlyDraggedItemIndex].ClearSlot();
            // Draw the new slot.
            this.inventorySlots[index].DrawSlot(inventoryItem);
        }
        else
        {
            // Redraw the old slot.
            this.inventorySlots[this.currentlyDraggedItemIndex].DrawSlot(slot.GetItem());
            // Redraw the new inventory slot.
            this.inventorySlots[index].DrawSlot(inventoryItem);
        }
        gameObject.GetComponentInParent<Inventory>().Modify(this.currentlyDraggedItemIndex, index);
    }

    private void HandleShowItemActions(InventorySlot slot)
    {
        Debug.Log(slot.name + " Right Mouse Button Clicked");
        // this.inventory.Remove(slot.GetItem().itemData);
    }
}