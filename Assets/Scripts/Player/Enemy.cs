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
        private PlayerCameraController _cameraController;
        SpriteRenderer SpriteRender;
        Animator Animator;
        private Vector2 _previousLocation;

        public Rigidbody RigidBody;
        public float MoveForce;
        private bool _possessed;
        private bool _chasingPlayer;

        void Awake()
        {
            _patrolStart = transform.position;
            _patrolToEnd = true;
            _chasingPlayer = false;
            _movementAI = new AStarPathfinding();    
        }

        void Start()
        {
            RigidBody = GetComponent<Rigidbody>();
            SpriteRender = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            Animator.SetInteger("State", 0);
            _executingTask = new SeekTask(_movementAI, this, _patrolEnd);
        }

        void FixedUpdate()
        {
            if (_possessed)
                return;

            CheckProximityToPlayers();

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

            float leftRight = _previousLocation.x - transform.position.x;
            AnimatorChangeState(1);
            if (leftRight > 0)
                SpriteRender.flipX = true;
            else if (leftRight < 0)
                SpriteRender.flipX = false;

            _previousLocation = transform.position;
        }

        private void CheckProximityToPlayers()
        {
            foreach (PlayerCTRL player in GameManager.Instance().GetListOfEntities())
            {
                //if (player == null) continue;
                if (_chasingPlayer == false && player.transform.position.y - 2 < transform.position.y &&
                    player.transform.position.y + 2 > transform.position.y &&
                    player.transform.position.x - 5 < transform.position.x &&
                    player.transform.position.x + 5 > transform.position.x)
                {
                    GameManager.Instance().GetSoundManager().StartAlertMusic();
                    _executingTask = new ChaseTask(player, this);
                    break;
                }
                else if (_chasingPlayer)
                {
                    _executingTask = new SeekTask(_movementAI, this, _patrolEnd);
                    _patrolToEnd = true;
                }
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

        private void AnimatorChangeState(int state)
        {
            Animator.SetInteger("State", state);
        }
    }
}