using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Flipper))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float _baseSpeedX = 0.5f;
    [SerializeField] private float _chaseSpeedX = 1.5f;

    private Flipper _flipper;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void Move(float direction, bool isChasing = false)
    {
        float speed = isChasing ? _chaseSpeedX : _baseSpeedX;

        _rigidbody.velocity = new Vector2(
                speed * direction * ConstantData.SpeedCoefficient, 
                _rigidbody.velocity.y);

        _flipper.Flip(direction);
    }

    public Vector2 GetVelocity()
    {
        return _rigidbody.velocity;
    }
}