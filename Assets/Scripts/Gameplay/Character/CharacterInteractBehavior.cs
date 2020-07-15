using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class CharacterInteractBehavior : MonoBehaviour
    {
        [SerializeField] private CharacterInventory _characterInventory = null;

        private readonly List<InteractableObject> _overlappedInteractableObjects = new List<InteractableObject>();

        public InteractableObject TryInteract()
        {
            if (_overlappedInteractableObjects.Count > 0)
            {
                var interactObject = _overlappedInteractableObjects[0];
                var _tag = interactObject.GetItemTag();
                if (_tag == null)
                {
                    interactObject.Interact();
                    _overlappedInteractableObjects.Remove(interactObject);
                }
                else
                {
                    var _item = _characterInventory.GetItem(_tag);
                    if (_item != null)
                    {
                        Destroy(_item.gameObject);
                        interactObject.Interact();
                        _overlappedInteractableObjects.Remove(interactObject);
                    }
                }
                            
                return interactObject;
            }

            return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            var itemComponent = other.GetComponentInParent<InteractableObject>();
            if (itemComponent != null)
            {
                _overlappedInteractableObjects.Add(itemComponent);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            var itemComponent = other.GetComponentInParent<InteractableObject>();
            if (itemComponent != null)
            {
                _overlappedInteractableObjects.Remove(itemComponent);

            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }
        }
    }
}
