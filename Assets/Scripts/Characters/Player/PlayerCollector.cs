using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<float> CoinCollected;
    public event Action<float> AidKitCollected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();

            if (collectable is Coin coin)
            {
                CoinCollected?.Invoke(collectable.Value);
                coin.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();

            if (collectable is AidKit aidKit)
            {
                AidKitCollected?.Invoke(collectable.Value);
                aidKit.gameObject.SetActive(false);
            }
        }
    }
}