namespace TopDownShooter.States
{
    public class TopDownStateBase : State
    {
        public Character Character { get; private set; }
        public Character Player { get; private set; }

        public TopDownStateBase(Character character, Character player)
        {
            Character = character;
            Player = player;
        }
    }
}
