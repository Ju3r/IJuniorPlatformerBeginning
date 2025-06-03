using UnityEngine;

[RequireComponent (typeof(InputReader), typeof(PlayerMover), typeof(PlayerAnimation))]
[RequireComponent (typeof(PlayerCollector), typeof(Health), typeof(PlayerAttack))]
public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector; 
    [SerializeField] private PlayerScore _score;
    [SerializeField] private PlayerAttackZone _attackZone;

    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimation _animation;
    private PlayerCollector _collector;
    private Health _health;
    private PlayerAttack _attack;

    private float _lackOfMovement = 0;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animation = GetComponent<PlayerAnimation>();
        _collector = GetComponent<PlayerCollector>();
        _health = GetComponent<Health>();
        _attack = GetComponent<PlayerAttack>();
    }

    private void OnEnable()
    {
        _collector.CoinCollected += OnCoinCollected;
        _collector.AidKitCollected += OnAidKitCollected;
        _inputReader.OnJumpPressed += OnJumpPressed;
        _attackZone.EnemyInAttackZone += StartAttack;
        _attackZone.EnemyExitAttackZone += StopAttack;
    }

    private void OnDisable()
    {
        _collector.CoinCollected -= OnCoinCollected;
        _collector.AidKitCollected -= OnAidKitCollected;
        _inputReader.OnJumpPressed -= OnJumpPressed;
        _attackZone.EnemyInAttackZone -= StartAttack;
        _attackZone.EnemyExitAttackZone -= StopAttack;
    }

    private void Update()
    {
        _animation.SetSpeed(Mathf.Abs(_inputReader.Direction));
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != _lackOfMovement)
            _mover.Move(_inputReader.Direction);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);

        if (_health.Value <= 0)
            Die();
    }

    private void StartAttack()
    {
        _attack.StartAttack();
    }

    private void StopAttack()
    {
        _attack.StopAttack();
    }

    private void OnAidKitCollected(float health)
    {
        _health.Add(health);
    }

    private void OnCoinCollected(float value)
    {
        _score.Add(value);
    }

    private void OnJumpPressed()
    {
        if (_groundDetector.IsGround)
            _mover.Jump();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}