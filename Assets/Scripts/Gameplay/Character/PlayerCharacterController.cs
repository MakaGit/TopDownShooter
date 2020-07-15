using UnityEngine;

namespace TopDownShooter
{
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(CharacterMovementBehaviour))]
    public class PlayerCharacterController : CharacterController
    {
        private CharacterMovementBehaviour _movementBehaviour = null;

        private void Awake()
        {
            Character = GetComponent<Character>();
            _movementBehaviour = GetComponent<CharacterMovementBehaviour>();

            InputManager.Instance.EventPlayerMovementDirectionChanged += OnPlayerMovementDirectionChanged;
            InputManager.Instance.EventPlayerShootingOccured += OnPlayerShootingOccured;
            InputManager.Instance.EventPlayerAimingOccured += OnPlayerAimingOccured;
            InputManager.Instance.EventPickUpItemButtonPressed += OnPickUpItemButtonPressed;
            InputManager.Instance.EventInteractButtonPressed += OnInteractButtonPressed;
            InputManager.Instance.EventPlayerLookPointChanged += OnPlayerLookPointChanged;
        }

        private void OnDestroy()
        {
            if (InputManager.TryInstance != null)
            {
                InputManager.Instance.EventPickUpItemButtonPressed -= OnPickUpItemButtonPressed;
                InputManager.Instance.EventPlayerShootingOccured -= OnPlayerShootingOccured;
                InputManager.Instance.EventPlayerAimingOccured -= OnPlayerAimingOccured;
                InputManager.TryInstance.EventPlayerMovementDirectionChanged -= OnPlayerMovementDirectionChanged;
                InputManager.Instance.EventInteractButtonPressed -= OnInteractButtonPressed;
                InputManager.Instance.EventPlayerLookPointChanged -= OnPlayerLookPointChanged;
            }
        }

        // DEBUG
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                Debug.Log("Equipping next weapon in inventory");
                var weapons = Character.Inventory.Items.FindAll(i => i.GetType() == typeof(Weapon));
                var currentWeaponIndex = weapons.IndexOf(Character.CurrentWeapon);
                var nextWeapon = weapons[(currentWeaponIndex + 1) % weapons.Count];
                nextWeapon.Apply(Character);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                var item = Character.Inventory.Items.Find(i => i != Character.CurrentWeapon);
                if (item != null)
                {
                    Debug.Log("Dropping item");
                    Character.Inventory.Drop(item);
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                var health = Character.GetComponent<CharacterHealthComponent>();
                health.ModifyHealth(-20);
            }
        }
        // end DEBUG

        private void OnPlayerMovementDirectionChanged(Vector3 targetDirection)
        {
            _movementBehaviour.ChangeCharacterMovementDirection(targetDirection);
        }

        private void OnPlayerLookPointChanged(Vector3 lookPoint)
        {
            _movementBehaviour.ChangeCharacterTargetLookPoint(lookPoint);
        }

        private void OnPlayerShootingOccured(bool isShooingStarted)
        {
            Character.Shoot(isShooingStarted);
        }

        private void OnPlayerAimingOccured(bool isAimigStarted)
        {
            Character.Aim(isAimigStarted);
        }

        private void OnPickUpItemButtonPressed()
        {
            var pickedUpItem = Character.TryPickUpItem();
        }

        private void OnInteractButtonPressed()
        {
            Character.TryInteract();
        }
    }
}
