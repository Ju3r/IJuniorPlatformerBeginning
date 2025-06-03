using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour 
{
    [SerializeField] private bool _isCollected = false;

    private Rigidbody2D _rigidbody;

    public event Action<Coin> Collected;

    [field: SerializeField] public float Value { get; private set; } = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform parent, Vector2 position)
    {
        float initAngularVelocity = 0f;
        _isCollected = false;

        transform.SetParent(parent);

        transform.position = position;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);

        if (_rigidbody == null)
            return;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = initAngularVelocity;
    }

    public void AddForce(Vector2 direction, float force, ForceMode2D forceMode)
    {
        _rigidbody.AddForce(direction * force, forceMode);
    }

    public void Collect()
    {
        if (_isCollected) 
            return;

        _isCollected = true;
        Collected?.Invoke(this);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}