using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private PlayerAnimation _animation;

    private LayerMask _enemyLayer;
    private bool _isActive = false;
    private bool _canAttack = true;

    private void Awake()
    {
        _enemyLayer = LayerMask.GetMask("Enemy");
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
                Collider2D hit = Physics2D.OverlapCircle(transform.position, _attackRange, _enemyLayer);

                if (hit != null && hit.TryGetComponent(out Enemy enemy))
                {
                    _animation.Attack();
                    enemy.TakeDamage(_damage);
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