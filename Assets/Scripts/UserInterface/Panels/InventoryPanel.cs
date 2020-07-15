using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownShooter
{
    public class InventoryPanel : UIPanel
    {
        [SerializeField] private WeaponListElement _weaponListElementPrefab = null;
        [SerializeField] private ListElement _listElementPrefab = null;

        [SerializeField] private RectTransform _listContentParent = null;

        [SerializeField] private CurrentWeaponInventorySlot _currentWeaponInventorySlot = null;
        [SerializeField] private UseItemInventorySlot _useItemInventorySlot = null;

        [SerializeField] private Slider _playerHealthProgressBar = null;
        [SerializeField] private Slider _playerEnergyProgressBar = null;

        private Character _character = null;
        private CharacterHealthComponent _playerHealthComponent = null;

        private Dictionary<InventoryItem, ListElement> _itemToListElementDictionary =
            new Dictionary<InventoryItem, ListElement>();

        private void Awake()
        {

            var playerCharacterController = FindObjectOfType<PlayerCharacterController>();
            _character = playerCharacterController.Character;
            _playerHealthComponent = playerCharacterController.GetComponent<CharacterHealthComponent>();
            _playerHealthComponent.EventHealthChanged += OnPlayerHealthChanged;

            _character.Inventory.EventItemPickedUp += OnPlayerPickedUpItem;
            _character.Inventory.EventItemDroppedDown += OnPlayerDroppedDownItem;

            _currentWeaponInventorySlot.EventWeaponElementAssigned += OnCurrentWeaponAssigned;
            _useItemInventorySlot.EventItemUse += OnItemUse;

            foreach (var item in _character.Inventory.Items)
            {
                var spawnedElement = SpawnInventoryItemListElement(item);

                if (spawnedElement.inventoryItem == _character.CurrentWeapon)
                {
                    _currentWeaponInventorySlot.AssignWeaponElement(spawnedElement);
                }
            }
        }

        private void OnEnable()
        {
            Time.timeScale = 0.1f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
        }

        private void OnDestroy()
        {
            if (_playerHealthComponent != null)
            {
                _playerHealthComponent.EventHealthChanged -= OnPlayerHealthChanged;
            }

            var playerCharacterController = FindObjectOfType<PlayerCharacterController>();
            if (playerCharacterController != null)
            {
                playerCharacterController.Character.Inventory.EventItemPickedUp -= OnPlayerPickedUpItem;
                playerCharacterController.Character.Inventory.EventItemDroppedDown -= OnPlayerDroppedDownItem;
            }

            _currentWeaponInventorySlot.EventWeaponElementAssigned -= OnCurrentWeaponAssigned;
            _useItemInventorySlot.EventItemUse -= OnItemUse;
        }

        private void OnPlayerHealthChanged(CharacterHealthComponent healthComponent, float health)
        {
            _playerHealthProgressBar.value = health / healthComponent.MaxHealth;

        }

        private ListElement SpawnInventoryItemListElement(InventoryItem item)
        {
            if (item.gameObject.CompareTag("Weapon"))
            {
                var spawnedElement = Instantiate(_weaponListElementPrefab);
                spawnedElement.transform.SetParent(_listContentParent);
                spawnedElement.SetInfo((InventoryItem)item);

                _itemToListElementDictionary.Add(item, spawnedElement);

                return spawnedElement;
            }
            else
            {
                var spawnedElement = Instantiate(_listElementPrefab);
                spawnedElement.transform.SetParent(_listContentParent);
                spawnedElement.SetInfo((InventoryItem)item);
                spawnedElement.SetChild(item);

              _itemToListElementDictionary.Add(item, spawnedElement);

              return spawnedElement;
            }
        }

        private void RemoveInventoryItemListElement(InventoryItem item)
        {
            Destroy(_itemToListElementDictionary[item].gameObject);
            _itemToListElementDictionary.Remove(item);
        }

        private void OnPlayerPickedUpItem(InventoryItem item)
        {
            SpawnInventoryItemListElement(item);
        }

        private void OnPlayerDroppedDownItem(InventoryItem item)
        {
            RemoveInventoryItemListElement(item);
        }

        private void OnCurrentWeaponAssigned(ListElement assignedWeaponElement, ListElement previousWeaponElement)
        {
            if (assignedWeaponElement == previousWeaponElement)
                return;

            assignedWeaponElement.inventoryItem.GetComponent<Weapon>().Apply(_character);

            previousWeaponElement.transform.SetParent(_listContentParent);
        }

        private void OnItemUse(ListElement listElement)
        {
            listElement.inventoryItem.Use(_character, listElement);
            //Destroy(listElement.gameObject);
        }

        public void OnResumeButtonClick()
        {
            UIManager.Instance.ShowPanel(UIPanelType.Gameplay);
        }
    }
}
