using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class WeaponListElement : ListElement, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Weapon weapon { get; private set; }
        public override void SetInfo(InventoryItem item)
        {
            weapon = item.GetComponent<Weapon>();

            _textLabel.text = weapon.Type.ToString();
        }
    }
}
