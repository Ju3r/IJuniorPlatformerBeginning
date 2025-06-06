using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private float _value = 0;

    public void Add(float value)
    {
        if (value < 0)
            return;

        _value += value;
    }
}