using UnityEngine;
using System;

/** CollectCoin is collectible (i.e. inherits from ICollectible). */
public class CollectCoin : MonoBehaviour, ICollectible
{
    public static event HandleCoinCollected OnCoinCollected;
    public delegate void HandleCoinCollected(ItemData itemData);
    public ItemData coinData;

    public void Collect()
    {
        Debug.Log("Coin Collected");
        Destroy(gameObject);
        OnCoinCollected?.Invoke(this.coinData);
    }
}