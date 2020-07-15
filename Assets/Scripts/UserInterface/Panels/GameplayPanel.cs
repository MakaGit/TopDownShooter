using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace TopDownShooter
{
    public class GameplayPanel : UIPanel
    {
        [SerializeField] private Text _scoreLabel = null;
        [SerializeField] private Slider _playerHealthProgressBar = null;
        [SerializeField] private Slider _playerEnergyProgressBar = null;
        [SerializeField] private NoiseAndGrain noiseAndGrain = null;

        // DEBUG
        private CharacterHealthComponent _playerHealthComponent = null;
        private int _currentScore = 0;

        private void Awake()
        {
            var characterController = FindObjectOfType<PlayerCharacterController>();
            _playerHealthComponent = characterController.GetComponent<CharacterHealthComponent>();
            _playerHealthComponent.EventHealthChanged += OnPlayerHealthChanged;

            CharacterHealthComponent.EventDeadDEBUG += OnCharacterDead;

            _scoreLabel.text = _currentScore.ToString();
        }

        private void OnDestroy()
        {
            if (_playerHealthComponent != null)
            {
                _playerHealthComponent.EventHealthChanged -= OnPlayerHealthChanged;
            }

            CharacterHealthComponent.EventDeadDEBUG -= OnCharacterDead;
        }

        private void OnPlayerHealthChanged(CharacterHealthComponent healthComponent, float health)
        {
            _playerHealthProgressBar.value = health / healthComponent.MaxHealth;

            noiseAndGrain.intensityMultiplier = 1.0f - (health / healthComponent.MaxHealth);

            if (health <= 0)
            {
                UIManager.Instance.ShowPanel(UIPanelType.Gameover);
            }
        }

        private void OnCharacterDead(CharacterHealthComponent healthComponent)
        {
            if (_playerHealthComponent != healthComponent)
            {
                _currentScore++;
                _scoreLabel.text = _currentScore.ToString();
            }
        }
        // end DEBUG

        public void OnPauseButtonClick()
        {
            UIManager.Instance.ShowPanel(UIPanelType.Pause);
        }

        public void OnInventoryButtonClick()
        {
            UIManager.Instance.ShowPanel(UIPanelType.Inventory);
        }
    }
}
