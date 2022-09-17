using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    // Sphere Cast All Export
    public List<GameObject> currentHitObjects = new List<GameObject>();


    private Vector3 _origin;
    private Vector3 _direction;

    private float _currentHitDistance;

    private void Update()
    {
        SphereCastAll();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, sphereRadius);
    }

    private void SphereCastAll()
    {
        _origin = transform.position;
        _direction = transform.forward;

        _currentHitDistance = maxDistance;
        currentHitObjects.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(_origin, sphereRadius, _direction, maxDistance, layerMask,
            QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            currentHitObjects.Add(hit.transform.gameObject);
            _currentHitDistance = hit.distance;
        }
    }

}
