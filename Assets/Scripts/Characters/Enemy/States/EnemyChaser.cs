using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterMover))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private LayerMask _raycastLayers;

    private bool _isActive = false;
    private CharacterMover _mover;
    private Transform _target;
    private Coroutine _coroutine;

    private void Awake()
    {
        _mover = GetComponent<CharacterMover>();
    }

    public void StartChase(Transform target)
    {
        if (gameObject.activeInHierarchy && _coroutine == null)
        {
            _isActive = true;
            _target = target;
            _coroutine = StartCoroutine(Chasing());
        }
    }

    public void StopChase()
    {
        _isActive = false;

        if (_coroutine != null)
        {
            StopCoroutine(Chasing());
            _coroutine = null;
        }
    }

    private IEnumerator Chasing()
    {
        bool _isChasing = true;

        while (_isActive && _target != null)
        {
            Vector2 offset = _target.position - transform.position;
            Vector2 direction = offset.normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _raycastDistance, _raycastLayers);

            if (hit.collider != null && hit.collider.transform == _target)
                _mover.Move(direction.x, _isChasing);

            yield return null;
        }

        _coroutine = null;
    }
}
