using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterAttackZone : MonoBehaviour
{
    public event Action InAttackZone;
    public event Action ExitAttackZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out _))
            InAttackZone?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out _))
            ExitAttackZone?.Invoke();
    }
}