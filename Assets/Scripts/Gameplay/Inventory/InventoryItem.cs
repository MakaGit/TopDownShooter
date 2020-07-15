using UnityEngine;

namespace TopDownShooter
{
    public abstract class InventoryItem : MonoBehaviour
    {
        [SerializeField] protected Transform _visualModel = null;
        [SerializeField] private Collider _pickUpTrigger = null;
        [SerializeField] private OutlineAddon outline = null;

        [SerializeField] public string nameObj = null;

        public virtual void ShowOutline()
        {
            outline.enabled = true;
        }

        public virtual void HideOutline()
        {
            outline.enabled = false;
        }
        public virtual void PickUp()
        {
            _visualModel.GetComponent<Rigidbody>().isKinematic = true;

            _visualModel.gameObject.SetActive(false);
            _pickUpTrigger.gameObject.SetActive(false);
        }

        public virtual void Drop(Vector3 dropPosition)
        {
            _visualModel.gameObject.SetActive(true);
            _pickUpTrigger.gameObject.SetActive(true);

            _visualModel.position = dropPosition;

            _visualModel.GetComponent<Rigidbody>().isKinematic = false;
        }

        public abstract void Apply(Character character);
        public virtual void Use(Character character, ListElement listElement) { }
        public virtual void Unapply() { }
    }
}
