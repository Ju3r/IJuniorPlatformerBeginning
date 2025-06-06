using UnityEngine;

[RequireComponent (typeof(InputReader), typeof(CharacterMover), typeof(CharacterAnimator))]
[RequireComponent (typeof(PlayerCollector), typeof(Health), typeof(CharacterAttacker))]
[RequireComponent (typeof(PlayerJumper))]
public class Player : Character
{
    [SerializeField] private GroundDetector _groundDetector; 
    [SerializeField] private PlayerScore _score;
    [SerializeField] private CharacterAttackZone _attackZone;

    private InputReader _inputReader;
    private CharacterMover _mover;
    private CharacterAnimator _animator;
    private PlayerCollector _collector;
    private CharacterAttacker _attacker;
    private PlayerJumper _jumper;

    private float _lackOfMovement = 0;

    protected override void Awake()
    {
        base.Awake();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<CharacterMover>();
        _animator = GetComponent<CharacterAnimator>();
        _collector = GetComponent<PlayerCollector>();
        _attacker = GetComponent<CharacterAttacker>();
        _jumper = GetComponent<PlayerJumper>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _collector.CoinCollected += OnCoinCollected;
        _collector.AidKitCollected += OnAidKitCollected;
        _inputReader.OnJumpPressed += OnJumpPressed;
        _attackZone.InAttackZone += StartAttack;
        _attackZone.ExitAttackZone += StopAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _collector.CoinCollected -= OnCoinCollected;
        _collector.AidKitCollected -= OnAidKitCollected;
        _inputReader.OnJumpPressed -= OnJumpPressed;
        _attackZone.InAttackZone -= StartAttack;
        _attackZone.ExitAttackZone -= StopAttack;
    }

    private void Update()
    {
        _animator.SetSpeed(Mathf.Abs(_inputReader.Direction));

        if (_inputReader.Direction != _lackOfMovement)
            _mover.Move(_inputReader.Direction);
    }

    private void StartAttack()
    {
        _attacker.StartAttack();
    }

    private void StopAttack()
    {
        _attacker.StopAttack();
    }

    private void OnAidKitCollected(float health)
    {
        Health.Add(health);
    }

    private void OnCoinCollected(float value)
    {
        _score.Add(value);
    }

    private void OnJumpPressed()
    {
        if (_groundDetector.IsGround)
            _jumper.Jump();
    }

    protected override void OnDeath()
    {
        gameObject.SetActive(false);
    }
}