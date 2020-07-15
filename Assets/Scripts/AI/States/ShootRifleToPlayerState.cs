namespace TopDownShooter.States
{
    public class ShootRifleToPlayerState : TopDownStateBase
    {
        private readonly AIMotor _motor;
        private readonly Character _character;


        public ShootRifleToPlayerState(Character character, Character player) : base(character, player)
        {
            _motor = character.GetComponent<AIMotor>();
            _character = character;
        }

        public override void OnStateEnter(IState fromState)
        {
            base.OnStateEnter(fromState);

            _motor.SetMovementToTarget(Character.transform.position);

            _character.AIShoot(true);
        }

        public override void OnStateExit(IState toState)
        {
            base.OnStateExit(toState);

            _character.AIShoot(false);
        }

        public override void Update()
        {
            base.Update();

            _motor.SetLookTarget(true, Player.transform.position);

            _character.AIShoot(true);

        }
    }
}