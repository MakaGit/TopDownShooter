using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5.0f;
    private Rigidbody _rigidbody;
    private Vector3 _currentVelocity = Vector3.zero;
    private Quaternion _currentRotation = Quaternion.identity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _currentVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _currentVelocity += Vector3.forward * _movementSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _currentVelocity += -Vector3.forward * _movementSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _currentVelocity += -Vector3.right * _movementSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _currentVelocity += Vector3.right * _movementSpeed;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, transform.position);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 lookPoint = ray.GetPoint(distance);
            _currentRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _currentVelocity.normalized * _movementSpeed;
        _rigidbody.rotation = _currentRotation;
    }
}
