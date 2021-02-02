using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] private Collider flightArea;
    [Range(0, 1)]
    [SerializeField] private float meanderiness;
    [SerializeField] private float randomDestinationDistance = 0.5f;
    [SerializeField] private float nearThreshold = 0.05f;
    // [SerializeField] private float rotationSpeed = 1f;
    
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private float speed = 0.1f;

    private Vector3 meander_wma = Vector3.zero;
    [Range(0, 1)]
    [SerializeField] private float meander_wma_w = .5f;
    
    private Vector3 destination;
    
    void Start()
    {
        destination = GetRandomPointInCollder(flightArea);
    }

    private Vector3 GetRandomPointInCollder(Collider collider)
    {
        if (Debug.isDebugBuild)
        {
            randomDestinationDistance = Mathf.Max(0.01f, randomDestinationDistance);
        }
        
        Vector3 point;
        do
        {
            // point = new Vector3(
            //     Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            //     Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            //     Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            // );
            point = transform.position + Random.rotationUniform * Vector3.up * randomDestinationDistance;
        } while( point != collider.ClosestPointOnBounds(point));

        return point;
    }
 
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = (destination - transform.position);
        if (difference.sqrMagnitude <= nearThreshold)
        {
            destination = GetRandomPointInCollder(flightArea);
        }

        var meander_rotation = noiseRotation();
        var new_meander = (meander_rotation * Vector3.up);
        meander_wma = new_meander * (1 - meander_wma_w) + meander_wma * (meander_wma_w);
        var meander = meanderiness * meander_wma;
        var direct = (1 - meanderiness) * difference.normalized;
        var nextStep = (meander + direct) * (speed * Time.deltaTime);
        _rigidbody.AddForce(nextStep);
        // transform.rotation = Quaternion.LookRotation(nextStep);
        if (_rigidbody.velocity.sqrMagnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    private Quaternion noiseRotation()
    {
        // Debug.Log(Mathf.PerlinNoise(Time.deltaTime*rotationSpeed, 1));
        return Random.rotation;
        // return Quaternion.Euler(
        //     360 * Mathf.PerlinNoise(Time.deltaTime*rotationSpeed, 1),
        //     360 * Mathf.PerlinNoise(Time.deltaTime*rotationSpeed, 2), 
        //     360 * Mathf.PerlinNoise(Time.deltaTime*rotationSpeed, 3)
        // );
    }
}
