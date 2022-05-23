using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text itemName;
    [SerializeField] 
    private TMP_Text itemDescription;

    public void ResetDescription()
    {
        // this.itemImage.gameObject.SetActive(false);
        this.itemImage.sprite = null;
        this.itemName.text = "";
        this.itemDescription.text = "";
    }

    public void SetDescription(Sprite sprite, string name, string description)
    {
        // this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemName.text = name;
        this.itemDescription.text = description;
    }
}
