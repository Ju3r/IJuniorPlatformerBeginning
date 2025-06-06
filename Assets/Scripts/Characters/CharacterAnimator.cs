using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _speedHash = Animator.StringToHash(ConstantData.SpeedParametr);
    private int _attackHash = Animator.StringToHash(ConstantData.AttackParametr);

    public void SetSpeed(float value)
    {
        _animator.SetFloat(_speedHash, value);
    }

    public void Attack()
    {
        _animator.SetTrigger(_attackHash);
    }
}