using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttackZone : MonoBehaviour
{
    public event Action PlayerInAttackZone;
    public event Action PlayerExitAttackZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            PlayerInAttackZone?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _))
            PlayerExitAttackZone?.Invoke();
    }
}