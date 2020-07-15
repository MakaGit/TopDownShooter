using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TopDownShooter
{
    public class CurrentWeaponInventorySlot : MonoBehaviour, IDropHandler
    {
        public event Action<ListElement, ListElement> EventWeaponElementAssigned;

        public ListElement AssignedElement { get; private set; }

        public void AssignWeaponElement(ListElement weaponElement)
        {
            AssignedElement = weaponElement;
            AssignedElement.transform.SetParent(transform);
            AssignedElement.transform.localPosition = Vector3.zero;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var weaponListElement = eventData.pointerDrag.GetComponent<WeaponListElement>();
                if (weaponListElement != null)
                {
                    EventWeaponElementAssigned?.Invoke(weaponListElement, AssignedElement);
                    AssignWeaponElement(weaponListElement);
                }
            }
        }
    }
}
