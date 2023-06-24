using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("GameObject")] [SerializeField]
    private GameObject Player;
    [Header("Offset")] [SerializeField] 
    private Vector2 _offset;
    
    private Vector2 _threshold;
    private float _speed = 3f;
    private Rigidbody2D _rb;
    private bool _hasRB;

    
    // Start is called before the first frame update
    void Start()
    {
        _threshold = calculateThreshold();
        _hasRB = Player.TryGetComponent(out _rb);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 follow = Player.transform.position;
        Vector2 positon = transform.position;
        Vector2 xDifferenceVector = Vector2.right * follow.x - Vector2.right * positon.x;
        float xDifference = xDifferenceVector.x;
        Vector2 yDifferenceVector = Vector2.up * follow.y - Vector2.up * positon.y;
        float yDifference = yDifferenceVector.y;

        Vector3 newPosition = positon;

        if (Math.Abs(xDifference) >= _threshold.x)
        {
            newPosition.x = follow.x;
        }
        
        if (Math.Abs(yDifference) >= _threshold.y)
        {
            newPosition.y = follow.y;
        }

        newPosition.z = -10;

        float moveSpeed = _rb.velocity.magnitude > _speed ? _rb.velocity.magnitude : _speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        
    }

    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height,
            Camera.main.orthographicSize);
        t.x -= _offset.x;
        t.y -= _offset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}