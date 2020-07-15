using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class EnergyCell : InventoryItem
    {
        public override void Apply(Character character)
        {
            _visualModel.gameObject.SetActive(true);

            _visualModel.SetParent(character.RightHandBone);
            _visualModel.localPosition = Vector3.zero;
            _visualModel.localRotation = Quaternion.identity;

            //character.ApplyWeapon(this);
        }
    }
}