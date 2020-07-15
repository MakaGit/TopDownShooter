using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(CharacterPickUpBehaviour))]
    [RequireComponent(typeof(CharacterInteractBehavior))]
    [RequireComponent(typeof(CharacterHealthComponent))]
    [RequireComponent(typeof(CharacterAnimator))]
    public class Character : MonoBehaviour
    {
        public Weapon CurrentWeapon { get; private set; }
        public CharacterInventory Inventory { get { return _inventoryComponent; } }

        public Transform RightHandBone { get { return _rightHandBone; } }

        [SerializeField] private CharacterInventory _inventoryComponent = null;

        [SerializeField] private Transform _rightHandBone = null;

        private CharacterPickUpBehaviour _pickUpComponent = null;
        private CharacterInteractBehavior _interactComponent = null;
        private CharacterHealthComponent _healthComponent = null;
        private CharacterAnimator _animatorComponent = null;

        private bool onAim = false;
        private bool onShooting = false;

        private void Awake()
        {
            _pickUpComponent = GetComponent<CharacterPickUpBehaviour>();
            _healthComponent = GetComponent<CharacterHealthComponent>();
            _animatorComponent = GetComponent<CharacterAnimator>();
            _interactComponent = GetComponent<CharacterInteractBehavior>();

            _healthComponent.EventDead += OnCharacterDead;
        }

        private void Start()
        {
            var defaultWeapon = GetComponentInChildren<Weapon>();
            if (defaultWeapon != null)
            {
                _inventoryComponent.PickUp(defaultWeapon);
                defaultWeapon.Apply(this);
            }
        }

        private void OnDestroy()
        {
            // ReSharper disable once DelegateSubtraction
            _healthComponent.EventDead -= OnCharacterDead;
        }

        public void Shoot(bool isShooingStarted)
        {
            if (CurrentWeapon != null)
            {
                if (!isShooingStarted)
                {
                    if (onAim)
                    {
                        _animatorComponent.SetAimingWeaponAnimation(CurrentWeapon.Type.GetAimingAnimationTriggerName());
                    }
                    else
                    {
                        _animatorComponent.SetIdleWeaponAnimation(CurrentWeapon.Type.GetIdleAnimationTriggerName());
                    }

                    CurrentWeapon.Shoot(isShooingStarted);
                    return;
                }

                CurrentWeapon.Shoot(isShooingStarted);
                _animatorComponent.SetAttackWeaponAnimation(CurrentWeapon.Type.GetAttackAnimationTriggerName());
            }
        }

        public void Aim(bool isAimingStarted)
        {
            if (!isAimingStarted)
            {
                onAim = false;
                _animatorComponent.SetIdleWeaponAnimation(CurrentWeapon.Type.GetIdleAnimationTriggerName());
                return;
            }

            onAim = true;
            _animatorComponent.SetAimingWeaponAnimation(CurrentWeapon.Type.GetAimingAnimationTriggerName());
        }

        public bool Reload(string tag)
        {
            var item =_inventoryComponent.GetItem(tag);
            if (item != null)
            {
                Destroy(item);
                _animatorComponent.SetReloadingWeaponAnimation(CurrentWeapon.Type.GetReloadingAnimationTriggerName());
                return true;
            }
            else
            {
                return false;
            }
        }

        public InventoryItem TryPickUpItem()
        {
            var pickedUpItem = _pickUpComponent.TryPickUpItem();
            if (pickedUpItem != null)
            {
                _inventoryComponent.PickUp(pickedUpItem);
            }

            return pickedUpItem;
        }

        public void TryInteract()
        {
            var interactedObj = _interactComponent.TryInteract();
        }

        private void OnCharacterDead(CharacterHealthComponent healthComponent)
        {
            _animatorComponent.Die();

            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CharacterMovementBehaviour>().enabled = false;
            GetComponent<AudioSource>().enabled = false;

            if (TryGetComponent(out AICharacterController ai))
                ai.enabled = false;

            var colliders = GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }

        public void ApplyWeapon(Weapon weapon)
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Unapply();
            }

            CurrentWeapon = weapon;

            // Start new idle animation
            _animatorComponent.SetIdleWeaponAnimation(CurrentWeapon.Type.GetIdleAnimationTriggerName());
            _animatorComponent.LeftHandIKTarget = CurrentWeapon.LeftHandIKTarget;
            _animatorComponent.SetLocomotionRifle();
        }


        //AI

        public void AIShoot(bool isShooingStarted)
        {
            if (CurrentWeapon != null)
            {
                if (!isShooingStarted)
                {
                    if (onAim)
                    {
                        _animatorComponent.SetAimingWeaponAnimation(CurrentWeapon.Type.GetAimingAnimationTriggerName());
                    }
                    else
                    {
                        _animatorComponent.SetIdleWeaponAnimation(CurrentWeapon.Type.GetIdleAnimationTriggerName());
                    }

                    CurrentWeapon.AIShoot(isShooingStarted);
                    return;
                }

                CurrentWeapon.AIShoot(isShooingStarted);
                _animatorComponent.SetAttackWeaponAnimation(CurrentWeapon.Type.GetAttackAnimationTriggerName());
            }
        }
    }
}
