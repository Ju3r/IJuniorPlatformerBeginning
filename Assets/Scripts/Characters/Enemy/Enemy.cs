using UnityEngine;

[RequireComponent(typeof(EnemyChaser), typeof(EnemyPatroller), typeof(CharacterAttacker))]
[RequireComponent(typeof(Health), typeof(CharacterAnimator), typeof(CharacterMover))]
public class Enemy : Character
{
    [SerializeField] private EnemyVisioner _visioner;
    [SerializeField] private CharacterAttackZone _attackZone;

    private EnemyChaser _chase;
    private EnemyPatroller _patroller;
    private CharacterAttacker _attacker;
    private Health _health;
    private Transform _currentTarget;
    private CharacterAnimator _animator;
    private CharacterMover _mover;

    protected override void Awake()
    {
        base.Awake();
        _chase = GetComponent<EnemyChaser>();
        _patroller = GetComponent<EnemyPatroller>();
        _attacker = GetComponent<CharacterAttacker>();
        _health = GetComponent<Health>();
        _animator = GetComponent<CharacterAnimator>();
        _mover = GetComponent<CharacterMover>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _visioner.PlayerDetected += StartChase;
        _visioner.PlayerEscaped += StopChase;
        _attackZone.InAttackZone += StartAttack;
        _attackZone.ExitAttackZone += OnPlayerExitAttackZone;
        _health.Die += OnDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();   
        _visioner.PlayerDetected -= StartChase;
        _visioner.PlayerEscaped -= StopChase;
        _attackZone.InAttackZone -= StartAttack;
        _attackZone.ExitAttackZone -= OnPlayerExitAttackZone;
        _health.Die -= OnDeath;
    }

    private void Start()
    {
        _patroller.Init();
        _patroller.StartPatrol();
    }

    private void Update()
    {
        _animator.SetSpeed(Mathf.Abs(_mover.GetVelocity().x));
    }

    private void StartChase(Transform target)
    {
        _currentTarget = target;

        _patroller.StopPatrol();
        _chase.StartChase(target);
    }

    private void StopChase()
    {
        _currentTarget = null;

        _chase.StopChase();
        _patroller.StartPatrol();
    }

    private void StartAttack()
    {
        _patroller.StopPatrol();
        _chase.StopChase();
        _attacker.StartAttack();
    }

    private void OnPlayerExitAttackZone()
    {
        _attacker.StopAttack();

        if (_currentTarget != null && _visioner.IsPlayerInVision(_currentTarget))
        {
            _chase.StartChase(_currentTarget);
        }
        else
        {
            _patroller.StartPatrol();
        }
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}