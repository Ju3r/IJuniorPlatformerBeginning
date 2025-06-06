using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AidKit : MonoBehaviour, ICollectable
{
    [field: SerializeField] public float Value { get; private set; } = 25f;

    private bool _isCollected = false;

    public void Collect()
    {
        if (_isCollected) 
            return;

        _isCollected = true;
    }
}