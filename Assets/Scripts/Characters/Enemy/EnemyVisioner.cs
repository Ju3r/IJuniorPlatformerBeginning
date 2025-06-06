using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyVisioner : MonoBehaviour
{
    [SerializeField] private float _distanceSqr = 10f;
    [SerializeField] private float _chaseDelay = 2f;
    [SerializeField] private LayerMask _visionLayers;

    private Coroutine _chaseCoroutine;

    public event Action<Transform> PlayerDetected;
    public event Action PlayerEscaped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerDetected?.Invoke(player.transform);

            if (_chaseCoroutine != null)
            {
                StopCoroutine(_chaseCoroutine);
                _chaseCoroutine = null;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_chaseCoroutine == null && gameObject.activeInHierarchy)
            _chaseCoroutine = StartCoroutine(ChaseDelayCoroutine());
    }

    private IEnumerator ChaseDelayCoroutine()
    {
        yield return new WaitForSeconds(_chaseDelay);

        PlayerEscaped?.Invoke();
        _chaseCoroutine = null;
    }

    public bool IsPlayerInVision(Transform target)
    {
        if (target == null) return false;

        Vector2 offset = target.position - transform.position;
        float distanceSqr = offset.sqrMagnitude;

        if (distanceSqr > _distanceSqr) 
            return false;

        Vector2 direction = offset.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceSqr, _visionLayers);
        
        return hit.collider != null && hit.collider.transform == target;
    }
}