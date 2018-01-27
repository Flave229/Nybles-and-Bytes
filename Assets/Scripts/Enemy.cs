using Assets.Scripts.AI;
using Assets.Scripts.AI.TaskSystem;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private ITask _executingTask;
        private IMovementAI _movementAI;

        public Vector3 SeekPosition;

        void Awake()
        {
            _movementAI = new AStarPathfinding();    
        }

        void Start()
        {
            _executingTask = new SeekTask(_movementAI, this, SeekPosition);
        }

        void Update()
        {
            if (_executingTask.IsComplete() == false)
                _executingTask.Execute();
        }
    }
}