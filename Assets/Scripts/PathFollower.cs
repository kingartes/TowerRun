using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PathCreator _pathCreator;

    private Rigidbody _rigidbody;
    private float _distanceTravelled = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var pathPoint = _pathCreator.path.GetPointAtDistance(_distanceTravelled);
        _rigidbody.MovePosition(_pathCreator.path.GetPointAtDistance(_distanceTravelled));
    }

    private void Update()
    {
        _distanceTravelled += Time.deltaTime * _speed;
     
        var nextPoint = _pathCreator.path.GetPointAtDistance(_distanceTravelled, EndOfPathInstruction.Loop);
        nextPoint.y = transform.position.y;

        transform.LookAt(nextPoint);
        _rigidbody.MovePosition(nextPoint);
    }

}
