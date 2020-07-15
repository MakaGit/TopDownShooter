using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public abstract class AIMotor : MonoBehaviour
    {
        public bool IsFreeLookEnabled { get; private set; }
        public Vector3 TargetMovementPoint { get; private set; }
        public Vector3 FreeLookTarget { get; private set; }

        public virtual void SetMovementToTarget(Vector3 targetMovementPoint)
        {
            TargetMovementPoint = targetMovementPoint;
        }

        public virtual void SetLookTarget(bool isFreeLookEnabled, Vector3 lookTarget)
        {
            IsFreeLookEnabled = isFreeLookEnabled;
            FreeLookTarget = lookTarget;
        }
    }
}