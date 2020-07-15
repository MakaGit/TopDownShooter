using System.Collections.Generic;
using System.Linq;

namespace TopDownShooter
{
    public class PlayerDetectedTransition : Transition<IState, IState>
    {
        private readonly bool _transitWhenDetected;

        private Character _playerCharacter;
        private readonly List<AISensor> _sensors;

        public PlayerDetectedTransition(bool transitWhenDetected, Character character, Character player, IState ownerState, IState targetState, int priority = 0) : base(ownerState, targetState, priority)
        {
            _transitWhenDetected = transitWhenDetected;
            _sensors = character.GetComponentsInChildren<AISensor>().ToList();
            _playerCharacter = player;
        }

        public override bool CanTransit()
        {
            bool isPlayerDetected = _sensors.Exists(s => s.IsTargetDetected(_playerCharacter.transform));
            return _transitWhenDetected ? isPlayerDetected : !isPlayerDetected;
        }
    }
}