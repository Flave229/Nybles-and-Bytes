using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts
{
    class UnityAINode : MonoBehaviour
    {
        public Node AINode;

        void Awake()
        {
            AINode = new Node
            {
                Position = transform.position
            };
        }
    }
}