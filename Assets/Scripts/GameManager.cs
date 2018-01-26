using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private AIDirector _aiDirector;

        void Awake()
        {
            _aiDirector = new AIDirector();
        }

        void Update()
        {
            _aiDirector.Update();
        }
    }
}
