using Assets.Scripts.AI;
using Assets.Scripts.AI.TaskSystem;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private ITask _executingTask;
        private IMovementAI _movementAI;

        public Rigidbody RigidBody;
        public Vector3 SeekPosition;
        public float MoveForce;

        void Awake()
        {
            _movementAI = new AStarPathfinding();    
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody>();
            _executingTask = new SeekTask(_movementAI, this, SeekPosition);
        }

        void Update()
        {
            RigidBody.AddForce(Vector3.down * 20.0f * RigidBody.mass);

            if (_executingTask.IsComplete() == false)
                _executingTask.Execute();
        }
    }
}