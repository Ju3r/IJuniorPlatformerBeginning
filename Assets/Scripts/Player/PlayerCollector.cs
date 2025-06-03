using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<float> CoinCollected;
    public event Action<float> AidKitCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coin.Collect();
            CoinCollected?.Invoke(coin.Value);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AidKit aidKit))
        {
            aidKit.Collect();
            AidKitCollected?.Invoke(aidKit.HealingValue);
        }
    }
}