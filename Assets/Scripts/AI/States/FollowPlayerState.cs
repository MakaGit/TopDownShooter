namespace TopDownShooter.States
{
    public class FollowPlayerState : TopDownStateBase
    {
        private readonly AIMotor _motor;

        public FollowPlayerState(Character character, Character player) : base(character, player)
        {
            _motor = character.GetComponent<AIMotor>();
        }

        public override void Update()
        {
            base.Update();

            _motor.SetMovementToTarget(Player.transform.position);
        }
    }
}