using UnityEngine;

public class Flipper : MonoBehaviour
{
    private float _lackOfMovement = 0f;
    float _turnRight = 0f;
    float _turnLeft = 180f;

    public void Flip(float direction)
    {
        float rotationY = direction > _lackOfMovement ? _turnRight: _turnLeft;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
