using System;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class CharacterInventory : MonoBehaviour
    {
        public event Action<InventoryItem> EventItemPickedUp;
        public event Action<InventoryItem> EventItemDroppedDown;

        public readonly List<InventoryItem> Items = new List<InventoryItem>();

        [SerializeField] private Transform _dropPoint = null;

        public void PickUp(InventoryItem item)
        {
            Items.Add(item);

            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.PickUp();

            EventItemPickedUp?.Invoke(item);
            Debug.Log("EventItemPickedUp");
        }

        public void Drop(InventoryItem item)
        {
            Items.Remove(item);

            item.transform.SetParent(null);

            item.Drop(_dropPoint.position);

            EventItemDroppedDown?.Invoke(item);
        }

        public InventoryItem GetItem(string tag)
        {
            foreach (InventoryItem _item in Items)
            {    
                if (_item.CompareTag(tag))
                {
                    var bo = Items.Remove(_item);
                    return _item;
                }
            }

            return null;
        }
    }
}
