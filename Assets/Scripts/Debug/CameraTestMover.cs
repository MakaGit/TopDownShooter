using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestMover : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 5.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * _movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * _movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * _movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * _movementSpeed * Time.deltaTime);
        }
    }

}
