using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    
    [Header("Sphere Cast Non Aloc Limit")] 
    public int sphereCastLimit;

    [Header("Warning!!!")] 
    [Header("Just choose one of these")]
    public bool isUsingSphereCast;
    public bool isUsingSphereCastAll;
    public bool isUsingSphereCastNonAloc;

    [Header("Sphere Cast Export")]
    public GameObject currentHitObject;

    [Header("Sphere Cast All and Sphere Cast Non Aloc Export")]
    public List<GameObject> currentHitObjects = new List<GameObject>();
    
    private Vector3 _origin;
    private Vector3 _direction;
    private float _currentHitDistance;

    private void Update()
    {
        Initialize();
        
        if (isUsingSphereCast)
            SphereCast();
        if (isUsingSphereCastAll)
            SphereCastAll();
        if (isUsingSphereCastNonAloc)
            SphereCastNonAloc();
    }

    private void Initialize()
    {
        _origin = transform.position;
        _direction = transform.forward;
    }

    private void SphereCast()
    {
        RaycastHit raycastHit;
        if (Physics.SphereCast(_origin, sphereRadius, _direction, out raycastHit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = raycastHit.transform.gameObject;
            _currentHitDistance = raycastHit.distance;
        }
        else
        {
            currentHitObject = null;
            _currentHitDistance = maxDistance;
        }
    }
    
    private void SphereCastAll()
    {
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

    private void SphereCastNonAloc()
    {
        _currentHitDistance = maxDistance;
        currentHitObjects.Clear();
        
        RaycastHit[] raycastHits = new RaycastHit[sphereCastLimit];
        var numberOfObjects = Physics.SphereCastNonAlloc(_origin, sphereRadius, _direction, raycastHits, maxDistance,
            layerMask, QueryTriggerInteraction.UseGlobal);
        for (var i = 0; i < numberOfObjects; i++)
        {
            currentHitObjects.Add(raycastHits[i].transform.gameObject);
            _currentHitDistance = raycastHits[i].distance;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, sphereRadius);
    }

}
