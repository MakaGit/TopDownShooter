using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(CharacterMovementBehaviour))]
    public class SimpleMovementInTargetDirectionAIMotor : AIMotor
    {
        private CharacterMovementBehaviour _movementBehaviour = null;
        private void Awake()
        {
            _movementBehaviour = GetComponent<CharacterMovementBehaviour>();
        }
        private void Update()
        {
            if ((TargetMovementPoint - transform.position).magnitude > 1.0f)
            {
                var movementDirection = (TargetMovementPoint - transform.position).normalized;
                _movementBehaviour.ChangeCharacterMovementDirection(movementDirection);
                if (!IsFreeLookEnabled)
                {
                    _movementBehaviour.ChangeCharacterTargetLookPoint(TargetMovementPoint);
                }
            }
            else
            {
                _movementBehaviour.ChangeCharacterMovementDirection(Vector3.zero);
            }

            if (IsFreeLookEnabled)
            {
                _movementBehaviour.ChangeCharacterTargetLookPoint(FreeLookTarget);
            }
        }
    }
}