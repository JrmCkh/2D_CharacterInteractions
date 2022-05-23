using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Every item is an ItemData. */
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public string itemDescription;
}