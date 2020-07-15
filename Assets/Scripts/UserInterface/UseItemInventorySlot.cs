using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TopDownShooter
{
    public class UseItemInventorySlot : MonoBehaviour, IDropHandler
    {
        public event Action<ListElement> EventItemUse;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                var listElement = eventData.pointerDrag.GetComponent<ListElement>();
                if (listElement != null)
                {
                    EventItemUse?.Invoke(listElement);
                }
            }
        }
    }
}