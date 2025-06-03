using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _attackHash = Animator.StringToHash(ConstantData.AttackParametr);

    public void Attack()
    {
        _animator.SetTrigger(_attackHash);
    }
}
