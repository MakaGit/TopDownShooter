using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class ListElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public InventoryItem inventoryItem { get; private set; }

        [SerializeField] private CanvasGroup _canvasGroup = null;
        [SerializeField] protected Text _textLabel = null;

        private Transform _initialParentTransform = null;
        private InventoryItem child = null;

        public virtual void SetInfo(InventoryItem item)
        {
            inventoryItem = item;
            _textLabel.text = item.nameObj.ToString();
        }

        public virtual void SetChild(InventoryItem item)
        {
            child = item;
        }

        public virtual InventoryItem GetChild()
        {
            return child;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _initialParentTransform = transform.parent;

            transform.SetParent(GetComponentInParent<Canvas>().transform);
            _canvasGroup.blocksRaycasts = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            if (transform.parent == GetComponentInParent<Canvas>().transform)
            {
                transform.SetParent(_initialParentTransform);
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
