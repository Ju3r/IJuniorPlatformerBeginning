using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _groundCounter = 0;

    public bool IsGround => _groundCounter > 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _groundCounter++;
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            _groundCounter--;

        if (_groundCounter < 0)
            _groundCounter = 0;
    }
}