using Assets.Scripts.AI;
using Assets.Scripts.AI.TaskSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        private ITask _executingTask;
        private IMovementAI _movementAI;
        private Vector3 _patrolStart;
        [SerializeField]
        private Vector3 _patrolEnd;
        private bool _patrolToEnd;

        public Rigidbody RigidBody;
        public float MoveForce;
        private bool _possessed;

        void Awake()
        {
            _patrolStart = transform.position;
            _patrolToEnd = true;
            _movementAI = new AStarPathfinding();    
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody>();
            _executingTask = new SeekTask(_movementAI, this, _patrolEnd);
        }

        void FixedUpdate()
        {
            if (_possessed) return;

            RigidBody.AddForce(Vector3.down * 20.0f * RigidBody.mass);

            if (_executingTask.IsComplete() == false)
                _executingTask.Execute();
            else if (_patrolToEnd == false)
            {
                _executingTask = new SeekTask(_movementAI, this, _patrolEnd);
                _patrolToEnd = true;
            }
            else
            {
                _executingTask = new SeekTask(_movementAI, this, _patrolStart);
                _patrolToEnd = false;
            }
                
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