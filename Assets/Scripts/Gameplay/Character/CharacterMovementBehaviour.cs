using UnityEngine;

namespace TopDownShooter
{
    public class CharacterMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = null;
        [SerializeField] private Animator _animator = null;

        [SerializeField] private float _movementSpeed = 5.0f;
        [SerializeField] private float _velocityChangeLerpSpeed = 5.0f;

        private Vector3 _targetMovementVelocity = Vector3.zero;
        private Vector3 _currentMovementVelocity = Vector3.zero;

        private Quaternion _rotation = Quaternion.identity;

        [SerializeField] private AudioSource _audioSource = null;

        public void ChangeCharacterMovementDirection(Vector3 targetDirection)
        {
            _targetMovementVelocity = targetDirection * _movementSpeed;
        }

        public void ChangeCharacterTargetLookPoint(Vector3 lookPoint)
        {
            lookPoint.y = transform.position.y;
            var lookVector = (lookPoint - transform.position).normalized;
            _rotation = Quaternion.LookRotation(lookVector);
        }

        private void Update()
        {
            _currentMovementVelocity = Vector3.Lerp(_currentMovementVelocity, _targetMovementVelocity, _velocityChangeLerpSpeed * Time.deltaTime);

            var lookVector = transform.forward;

            float moveSpeedX = Vector3.Dot(_currentMovementVelocity / _movementSpeed, -Vector3.Cross(lookVector, Vector3.up));
            float moveSpeedZ = Vector3.Dot(_currentMovementVelocity / _movementSpeed, lookVector);

            if (_targetMovementVelocity == Vector3.zero)
            {
                _audioSource.Pause();
            }
            else
            {
                _audioSource.UnPause();
            }

            _animator.SetFloat("MoveSpeedX", moveSpeedX);
            _animator.SetFloat("MoveSpeedZ", moveSpeedZ);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("AttackTrigger");
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(_currentMovementVelocity.x, _rigidbody.velocity.y, _currentMovementVelocity.z);
            _rigidbody.rotation = _rotation;
        }
    }
}