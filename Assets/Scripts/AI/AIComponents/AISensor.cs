using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public abstract class AISensor : MonoBehaviour
    {
        public abstract bool IsTargetDetected(Transform target);
    }
}