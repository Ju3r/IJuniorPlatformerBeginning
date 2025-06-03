using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 10f;

    private LayerMask _raycastLayers;
    private bool _isActive = false;
    private EnemyMover _mover;
    private Transform _target;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _raycastLayers = LayerMask.GetMask("Player", "Obstacle");
    }

    public void StartChase(Transform target)
    {
        _isActive = true;
        _target = target;
        StartCoroutine(Chasing());
    }

    public void StopChase()
    {
        _isActive = false;
        StopCoroutine(Chasing());
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
    }
}
