using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    /** Item data */
    private InventoryItem item;

    /** Image of item. */
    [SerializeField]
    private Image itemImage;

    /** Image of selected item (an highlighted border) */
    [SerializeField]
    private Image borderImage;

    /** Text to represent the number of items in stack. */
    [SerializeField] 
    private TMP_Text stackSizeText;
    
    /** Strings to represent the name and description of item */
    private string itemName, itemDescription;

    public event Action<InventorySlot> OnItemClicked, 
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, 
        OnRightMouseBtnClick;

    private bool empty = true;

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }
        this.item = item;
        this.itemImage.enabled = true;
        this.stackSizeText.enabled = true;

        this.itemImage.sprite = item.itemData.icon;
        this.stackSizeText.text = item.stackSize.ToString();

        this.itemName = item.itemData.itemName;
        this.itemDescription = item.itemData.itemDescription;
        this.empty = false;
    }

    public void ClearSlot()
    {
        this.itemImage.enabled = false;
        this.stackSizeText.enabled = false;
        this.empty = true;
    }

    /** Getter method for item */
    public InventoryItem GetItem()
    {
        return this.item;
    }

    /** Getter method for item's image. */
    public Sprite GetImage()
    {
        return this.itemImage.sprite;
    }

    /** Getter method for item's name. */
    public string GetName()
    {
        return this.itemName;
    }

    /** Getter method for item's description. */
    public string GetDescription()
    {
        return this.itemDescription;
    }

    /** Getter method for item's quantity. */
    public string GetQuantity()
    {
        return this.stackSizeText.text;
    }

    public void Select()
    {
        this.borderImage.enabled = true;
    }

    public void Deselect()
    {
        this.borderImage.enabled = false;
    }

    /** Returns true if slot is selected; otherwise false. */
    public bool IsSelected()
    {
        return this.borderImage.enabled;
    }

    /** Returns true if slot is empty; otherwise false. */
    public bool IsEmpty()
    {
        return this.empty;
    }

    public void OnBeginDrag()
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        if (empty)
            return;
        PointerEventData pointerData = (PointerEventData) data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}