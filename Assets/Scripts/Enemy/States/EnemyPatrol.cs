using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;
    [SerializeField] private float _minInaccuracySqr = 0.4f;

    private bool _isActive = false;
    private EnemyMover _mover;
    private Transform _targetPoint;
    private int _currentPointIndex = 0;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    public void Init()
    {
        _targetPoint = _waypoints[_currentPointIndex].transform;
    }

    public void StartPatrol()
    {
        if (gameObject.activeInHierarchy)
        {
            _isActive = true;
            StartCoroutine(Patrolling());
        }
    }

    public void StopPatrol()
    {
        _isActive = false;
        StopCoroutine(Patrolling());
    }

    private IEnumerator Patrolling()
    {
        while (_isActive && _targetPoint != null)
        {
            Vector2 offset = _targetPoint.position - transform.position;
            Vector2 direction = offset.normalized;
            float distanceSqr = offset.sqrMagnitude;

            if (distanceSqr < _minInaccuracySqr)
            {
                _currentPointIndex = ++_currentPointIndex % _waypoints.Length;
                _targetPoint = _waypoints[_currentPointIndex].transform;
            }

            _mover.Move(direction.x);

            yield return null;
        }
    }
}