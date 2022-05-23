using UnityEngine;
using System;

/** CollectConsumable is collectible (i.e. inherits from ICollectible). */
public class CollectConsumable : MonoBehaviour, ICollectible
{
    public static event HandleConsumableCollected OnConsumableCollected;
    public delegate void HandleConsumableCollected(ItemData itemData);
    public ItemData consumableData;

    public void Collect()
    {
        Debug.Log("Consumable Collected");
        Destroy(gameObject);
        OnConsumableCollected?.Invoke(this.consumableData);
    }
}