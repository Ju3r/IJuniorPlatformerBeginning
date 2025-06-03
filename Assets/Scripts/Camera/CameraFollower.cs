using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3 (0, 5, -7);
    [SerializeField] private Transform _player;

    private void LateUpdate()
    {
        transform.position = _player.position + _offset;
    }
}