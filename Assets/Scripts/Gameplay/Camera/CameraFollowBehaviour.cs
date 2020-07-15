using UnityEngine;

namespace TopDownShooter
{
    public class CameraFollowBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;

        [SerializeField] private Vector3 _positionOffset = Vector3.zero;
        [SerializeField] private Vector3 _axisAnglesOffset = Vector3.zero;

        [SerializeField] private float _positionLerpSpeed = 5.0f;

        private void Start()
        {
            transform.position = _target.position + _positionOffset;
            transform.rotation = Quaternion.Euler(_axisAnglesOffset);
        }

        private void Update()
        {
            Vector3 targetPosition = _target.position + _positionOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _positionLerpSpeed * Time.deltaTime);
        }
    }
}