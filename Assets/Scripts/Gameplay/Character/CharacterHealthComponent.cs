using System;
using UnityEngine;

namespace TopDownShooter
{
    public class CharacterHealthComponent : MonoBehaviour
    {
        public static Action<CharacterHealthComponent> EventDeadDEBUG; // DEBUG

        public Action<CharacterHealthComponent> EventDead;
        public Action<CharacterHealthComponent, float> EventHealthChanged;

        public float MaxHealth { get { return _maxHealth; } }
        public float Health { get; private set; }

        [SerializeField] private float _maxHealth = 100f;

        private void Start()
        {
            Health = _maxHealth;
        }

        public void ModifyHealth(float healthPoints)
        {
            float newHealth = Mathf.Clamp(Health + healthPoints, 0f, _maxHealth);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (newHealth != Health)
            {
                Health = newHealth;
                EventHealthChanged?.Invoke(this, Health);

                if (Health <= 0f)
                {
                    EventDead?.Invoke(this);
                    EventDeadDEBUG?.Invoke(this);
                }
            }
        }
    }
}
