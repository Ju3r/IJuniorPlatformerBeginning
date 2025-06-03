using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AidKit : MonoBehaviour
{
    [field: SerializeField] public float HealingValue { get; private set; } = 25f;

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}