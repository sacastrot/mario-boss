using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    
    [Header("Movement")] 
    [SerializeField] private float _inverse = 1f;// degrees/sec
    
    [Space(20)]
    [SerializeField] private Transform _center;
    
    private float _movementSpeed = 30f;
    private float _radius;
    private float _angleMovementRad; //rad
    
    void Start()
    {
        _radius = Vector2.Distance(transform.position, _center.position);
    }

    // Update is called once per frame
    void Update()
    {
        _angleMovementRad += _inverse * _movementSpeed * Mathf.Deg2Rad * Time.deltaTime;
        
        CircleMovement();
    }

    void CircleMovement()
    {
        var offset = new Vector2(Mathf.Sin(_angleMovementRad), Mathf.Cos(_angleMovementRad)) * _radius;
        transform.position = _center.position + (Vector3)offset;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
