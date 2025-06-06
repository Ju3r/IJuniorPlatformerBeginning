using System.Collections;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private LayerMask _targetLayer;

    private bool _isActive = false;
    private bool _canAttack = true;
    private Coroutine _coroutine;
    private Coroutine _cooldownCoroutine;

    public void StartAttack()
    {
        if (gameObject.activeInHierarchy && _coroutine == null)
        {
            _isActive = true;
            _canAttack = true;
            _coroutine = StartCoroutine(Attacking());
        }

        if (gameObject.activeInHierarchy && _cooldownCoroutine == null)
        {
            _cooldownCoroutine = StartCoroutine(AttackCooldown());
        }
    }

    public void StopAttack()
    {
        _isActive = false;
        _canAttack = false;

        if (_coroutine != null)
        {
            StopCoroutine(Attacking());
            _coroutine = null;
        }

        if (_cooldownCoroutine != null)
        {
            StopCoroutine(AttackCooldown());
            _cooldownCoroutine = null;
        }
    }

    private IEnumerator Attacking()
    {
        while (_isActive)
        {
            if (_canAttack)
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, _attackRange, _targetLayer);

                if (hit != null && hit.TryGetComponent(out IDamageable iDamageable))
                {
                    _animator.Attack();
                    iDamageable.TakeDamage(_damage);
                }

                StartCoroutine(AttackCooldown());
            }

            yield return null;
        }

        _coroutine = null;
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;

        yield return new WaitForSeconds(_attackCooldown);

        _canAttack = true;
        _cooldownCoroutine = null;
    }
}