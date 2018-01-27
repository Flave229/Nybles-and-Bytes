using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public AIDirector AiDirector;
        private static GameManager _instance;

        private GameManager() { }

        public static GameManager Instance()
        {
            return _instance ?? (_instance = new GameManager());
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(this.gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void Start()
        {
            AiDirector = new AIDirector();
        }

        void Update()
        {
            AiDirector.Update();
        }
    }
}
