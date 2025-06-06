using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action Die;

    [field: SerializeField] public float Value { get; private set; } = 100;

    public void Add(float value)
    {
        Value += value;
    }

    public void TakeDamage(float damage)
    {
        Value = Mathf.Max(0, Value - damage);

        if (Value == 0)
            Die?.Invoke();
    }
}