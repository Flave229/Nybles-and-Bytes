using UnityEngine;

namespace Assets.Scripts.AI.TaskSystem
{
    public class ChaseTask : ITask
    {
        private PlayerCTRL _player;
        private Enemy _character;
        private Vector2 _initialSpotLocation;
        private bool _completed;

        public ChaseTask(PlayerCTRL player, Enemy character)
        {
            _completed = false;
            _player = player;
            _character = character;
            _initialSpotLocation = new Vector2(_character.transform.position.x, _character.transform.position.y);
        }

        public void Execute()
        {
            if (Vector2.Distance(_initialSpotLocation, new Vector2(_player.transform.position.x, _player.transform.position.y)) > 5)
                SetCompleted();
            
            float leftRight = (_player.transform.position.x - _character.transform.position.x) > 0 ? 1 : -1;
            _character.RigidBody.MovePosition(_character.transform.position + (new Vector3(leftRight * Time.fixedDeltaTime * _character.MoveForce * 2, 0.0f, 0.0f) * Time.deltaTime));
        }

        public bool IsComplete()
        {
            return _completed;
        }

        public void SetCompleted()
        {
            _completed = true;
        }
    }
}