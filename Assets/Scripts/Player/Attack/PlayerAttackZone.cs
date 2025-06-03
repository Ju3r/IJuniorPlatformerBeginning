using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttackZone : MonoBehaviour
{
    public event Action EnemyInAttackZone;
    public event Action EnemyExitAttackZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out _))
            EnemyInAttackZone?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out _))
            EnemyExitAttackZone?.Invoke();
    }
}
