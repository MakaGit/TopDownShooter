using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] private Collider _interactableTrigger = null;

        public virtual void Interact() { }

        public virtual string GetItemTag() { return null; }
    }
}
