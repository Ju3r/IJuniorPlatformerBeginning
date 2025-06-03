using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Flipper))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _jumpForce = 10;

    private Flipper _flipper;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(
                _speedX * direction * ConstantData.SpeedCoefficient * Time.fixedDeltaTime, 
                _rigidbody.velocity.y);

        _flipper.Flip(direction);
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce);
    }
}