using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _jumpKey = KeyCode.W;

    public event Action OnJumpPressed;

    public float Direction {  get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(ConstantData.HorizontalAxis);

        if (Input.GetKeyDown(_jumpKey))
        {
            OnJumpPressed?.Invoke();
        }
    }
}
