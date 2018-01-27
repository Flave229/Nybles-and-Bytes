using Assets.Scripts.Event;

namespace Assets.Scripts.Commands
{
    public class PlayerTravelThroughDoor : ICommand
    {
        private Lift _thisLift;
        private Enemy _enemy;

        public PlayerTravelThroughDoor(Lift thisLift, Enemy enemy)
        {
            _thisLift = thisLift;
            _enemy = enemy;
        }

        public void Execute()
        {
        }
    }
}
