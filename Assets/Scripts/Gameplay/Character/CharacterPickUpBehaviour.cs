using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class CharacterPickUpBehaviour : MonoBehaviour
    {
        private readonly List<InventoryItem> _overlappedItems = new List<InventoryItem>();

        public InventoryItem TryPickUpItem()
        {
            if (_overlappedItems.Count > 0)
            {
                var item = _overlappedItems[0];
                _overlappedItems.Remove(item);
                item.HideOutline();
                return item;
            }

            return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            var itemComponent = other.GetComponentInParent<InventoryItem>();
            if (itemComponent != null)
            {
                itemComponent.ShowOutline();
                _overlappedItems.Add(itemComponent);

                // Show UI hint
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var itemComponent = other.GetComponentInParent<InventoryItem>();
            if (itemComponent != null)
            {
                itemComponent.HideOutline();
                _overlappedItems.Remove(itemComponent);

                if (_overlappedItems.Count == 0)
                {
                    // Hide UI hint
                }
            }
        }
    }
}
