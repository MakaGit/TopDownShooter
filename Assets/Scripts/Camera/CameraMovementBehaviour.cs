using UnityEngine;

public class CameraMovementBehaviour : MonoBehaviour
{

    [SerializeField] private Transform _target = null;
    [SerializeField] private Vector3 _offset = Vector3.zero;
    [SerializeField] private float _movementSpeed = 5.0f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _movementSpeed * Time.deltaTime);
    }

}
