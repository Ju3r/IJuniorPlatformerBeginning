using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private Transform _coinContainer;
    [SerializeField] private int _poolCapacity = 5;  
    [SerializeField] private int _poolMaxSize = 20;  
    [SerializeField] private float _minForce = 5f; 
    [SerializeField] private float _maxForce = 10f; 
    [SerializeField] private float _minAngle = 0f;  
    [SerializeField] private float _maxAngle = 360f; 
    [SerializeField] private float _spawnRate = 1f;

    private ObjectPool<Coin> _pool;
    private bool _isActive = true;
    private List<Coin> _allCoins = new List<Coin>();

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Create(),
            actionOnGet: coin => GetFromPool(coin),
            actionOnRelease: coin => coin.gameObject.SetActive(false),
            actionOnDestroy: coin => Destroy(coin),
            collectionCheck : true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        StartCoroutine(Spawning());
    }

    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnDisable()
    {
        foreach (Coin coin in _allCoins)
        {
            if (coin != null)
            {
                coin.Collected -= OnCollected;
            }
        }

        _allCoins.Clear();
        _isActive = false;
    }

    private Coin Create()
    {
        Coin coin = Instantiate(_prefab);
        _allCoins.Add(coin);

        return coin;
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds spawnWait = new WaitForSeconds(_spawnRate);

        while (_isActive)
        {
            if (_pool.CountActive < _poolMaxSize)
                _pool.Get();

            yield return spawnWait;
        }
    }

    private void GetFromPool(Coin coin)
    {
        coin.Init(_coinContainer, transform.position);
        coin.Collected += OnCollected;

        float angle = Random.Range(_minAngle, _maxAngle);
        float force = Random.Range(_minForce, _maxForce);

        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

        coin.AddForce(direction, force, ForceMode2D.Impulse);
    }

    private void OnCollected(Coin coin)
    {
        coin.Collected -= OnCollected;
        _pool.Release(coin);
    }

    private void Destroy(Coin coin)
    {
        coin.Collected -= OnCollected;
        _allCoins.Remove(coin);
        coin.Destroy();
    }
}