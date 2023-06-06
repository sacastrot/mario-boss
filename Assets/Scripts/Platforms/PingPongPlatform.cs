using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PingPongPlatform : MonoBehaviour
{
    
    [FormerlySerializedAs("_initPos")]
    [Space(20)]
    [SerializeField]
    private Transform _initPosTransform;
    [FormerlySerializedAs("_endPos")] [SerializeField]
    private Transform _endPosTransform;
    
    [Space(20)]
    [SerializeField]
    private float _speed = 1;
    
    private Vector3 _initPos, _endPos;

    void Start()
    {
        _initPos = _initPosTransform.position;
        _endPos = _endPosTransform.position;
        transform.position = _initPos;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float d = (_initPos - _endPos).magnitude;
        float delta = Mathf.PingPong(Time.time * _speed, d);
        transform.position = Vector3.Lerp(_initPos, _endPos, delta / d);
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
