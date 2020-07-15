using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class DetectingInsideRadiusAISensor : AISensor
    {
        [SerializeField] public float DetectionRadius = 10.0f;

        public override bool IsTargetDetected(Transform target)
        {
            return (transform.position - target.position).magnitude <= DetectionRadius;
        }
    }
}