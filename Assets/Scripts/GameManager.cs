using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public AIDirector AiDirector;
        private static GameManager _instance;
        private static List<PlayerCTRL> _mListOfPlayers;

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
            _mListOfPlayers = new List<PlayerCTRL>();
        }

        void Update()
        {
            AiDirector.Update();
        }

        public void AddEntityToList(PlayerCTRL entity)
        {
            if (_mListOfPlayers == null)
                _mListOfPlayers = new List<PlayerCTRL>();

            _mListOfPlayers.Add(entity);
        }

        public List<PlayerCTRL> GetListOfEntities()
        {
            return _mListOfPlayers;
        }
    }
}
