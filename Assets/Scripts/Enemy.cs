using Assets.Scripts.AI;
using Assets.Scripts.AI.TaskSystem;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        private ITask _executingTask;
        private IMovementAI _movementAI;

        public Rigidbody RigidBody;
        public Vector3 SeekPosition;
        public float MoveForce;
        private bool _possessed;

        void Awake()
        {
            _movementAI = new AStarPathfinding();    
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody>();
            _executingTask = new SeekTask(_movementAI, this, SeekPosition);
        }

        void FixedUpdate()
        {
            if (_possessed) return;

            RigidBody.AddForce(Vector3.down * 20.0f * RigidBody.mass);

            if (_executingTask.IsComplete() == false)
                _executingTask.Execute();
        }

        public void SetPossessed(bool possessed)
        {
            _possessed = possessed;
        }

        public bool GetPossessed()
        {
            return _possessed;
        }
    }
}