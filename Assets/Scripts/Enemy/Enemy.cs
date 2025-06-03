using System;
using UnityEngine;

[RequireComponent(typeof(EnemyChase), typeof(EnemyPatrol), typeof(EnemyAttack))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour 
{
    [SerializeField] private EnemyVision _vision;
    [SerializeField] private EnemyAttackZone _attackZone;

    private EnemyChase _chase;
    private EnemyPatrol _patrol;
    private EnemyAttack _attack;
    private Health _health;
    private Transform _currentTarget;

    private void Awake()
    {
        _chase = GetComponent<EnemyChase>();
        _patrol = GetComponent<EnemyPatrol>();
        _attack = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _vision.PlayerDetected += StartChase;
        _vision.PlayerEscaped += StopChase;
        _attackZone.PlayerInAttackZone += StartAttack;
        _attackZone.PlayerExitAttackZone += OnPlayerExitAttackZone;
    }

    private void OnDisable()
    {
        _vision.PlayerDetected -= StartChase;
        _vision.PlayerEscaped -= StopChase;
        _attackZone.PlayerInAttackZone -= StartAttack;
        _attackZone.PlayerExitAttackZone -= OnPlayerExitAttackZone;
    }

    private void Start()
    {
        _patrol.Init();
        _patrol.StartPatrol();
    }
    
    private void StartChase(Transform target)
    {
        _currentTarget = target;

        _patrol.StopPatrol();
        _chase.StartChase(target);
    }

    private void StopChase()
    {
        _currentTarget = null;

        _chase.StopChase();
        _patrol.StartPatrol();
    }

    private void StartAttack()
    {
        _patrol.StopPatrol();
        _chase.StopChase();
        _attack.StartAttack();
    }

    private void OnPlayerExitAttackZone()
    {
        _attack.StopAttack();

        if (_currentTarget != null && _vision.IsPlayerInVision(_currentTarget))
        {
            _chase.StartChase(_currentTarget);
        }
        else
        {
            _patrol.StartPatrol();
        }
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        if (_health.Value <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}