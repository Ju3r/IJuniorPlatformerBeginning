using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    protected Health Health;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        Health.Die += OnDeath;
    }

    protected virtual void OnDisable()
    {
        Health.Die -= OnDeath;
    }

    public void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
    }

    protected abstract void OnDeath();
}
