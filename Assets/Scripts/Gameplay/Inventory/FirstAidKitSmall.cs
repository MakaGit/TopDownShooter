using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class FirstAidKitSmall : InventoryItem
    {
        private float HPRestorePoints = 25.0f;
        public override void Use(Character character, ListElement listElement)
        {
            character.GetComponentInParent<CharacterHealthComponent>().ModifyHealth(HPRestorePoints);
            Destroy(listElement.gameObject);
            Destroy(gameObject);
        }

        public override void Apply(Character character)
        {
            throw new System.NotImplementedException();
        }
    }
}