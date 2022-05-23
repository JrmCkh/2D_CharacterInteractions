using UnityEngine;
using System;

/** CollectCrystal is collectible (i.e. inherits from ICollectible). */
public class CollectCrystal : MonoBehaviour, ICollectible
{
    public static event HandleCrystalCollected OnCrystalCollected;
    public delegate void HandleCrystalCollected(ItemData itemData);
    public ItemData crystalData;

    public void Collect()
    {
        Debug.Log("Crystal Collected");
        Destroy(gameObject);
        OnCrystalCollected?.Invoke(this.crystalData);
    }
}