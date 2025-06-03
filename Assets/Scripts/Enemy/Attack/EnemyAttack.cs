using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private EnemyAnimation _animation;

    private LayerMask _playerLayer;
    private bool _isActive = false;
    private bool _canAttack = true;

    private void Awake()
    {
        _playerLayer = LayerMask.GetMask("Player");
    }

    public void StartAttack()
    {
        _isActive = true;
        _canAttack = true;
        StartCoroutine(Attacking());
    }

    public void StopAttack()
    {
        _isActive = false;
        _canAttack = false;
        StopCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (_isActive)
        {
            if (_canAttack)
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, _attackRange, _playerLayer);

                if (hit != null && hit.TryGetComponent(out Player player))
                {
                    _animation.Attack();
                    player.TakeDamage(_damage);
                }

                StartCoroutine(AttackCooldown());
            }

            yield return null;
        }
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;

        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
    }
}