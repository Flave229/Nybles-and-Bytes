﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI.TaskSystem
{
    public class SeekTask : ITask
    {
        private IMovementAI _movementAI;
        private List<Node> _movementPath;
        private Enemy _character;
        private Vector2 _seekTo;
        private bool _complete;
        private bool _started;

        public SeekTask(IMovementAI movementAI, Enemy character, Vector2 seekLocation)
        {
            _movementAI = movementAI;
            _character = character;
            _seekTo = seekLocation;
            _movementPath = new List<Node>();
            _started = false;
            _complete = false;
        }

        public void Execute()
        {
            if (_started == false)
            {
                float distanceHeuristic = Vector2.Distance(_character.transform.position, _seekTo);
                Node source = new Node
                {
                    Position = _character.transform.position,
                    CurrentCost = 0.0f,
                    Heuristic = distanceHeuristic,
                    TotalCost = distanceHeuristic
                };

                Node target = new Node
                {
                    Position = _seekTo,
                    CurrentCost = 0.0f,
                    Heuristic = 0.0f,
                    TotalCost = 0.0f
                };

                // TODO: Need to add connected nodes to the source and target nodes. Can't do this until doors are done. 
                _movementPath = _movementAI.CreatePath(source, target);
            }

            if (CheckIfAtNextNode() == false)
                return;

            _movementPath.RemoveAt(_movementPath.Count - 1);
            
            if (_movementPath.Count <= 0)
            {
                SetCompleted();
                return;
            }
        }

        private bool CheckIfAtNextNode()
        {
            Vector3 playerPosition = _character.transform.position;
            Vector2 nextNodePosition = _movementPath[_movementPath.Count - 1].Position;
            if (playerPosition.x - 3 < nextNodePosition.x &&
                playerPosition.x + 3 > nextNodePosition.x &&
                playerPosition.y - 2 < nextNodePosition.y &&
                playerPosition.y + 2 > nextNodePosition.y)
            {
                return true;
            }

            return false;
        }

        public bool IsComplete()
        {
            return _complete;
        }

        public void SetCompleted()
        {
            _complete = true;
        }
    }
}