using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float Value { get; private set; } = 100;

    public void Add(float value)
    {
        Value += value;
    }

    public void TakeDamage(float value)
    {
        Value -= value;
    }
}