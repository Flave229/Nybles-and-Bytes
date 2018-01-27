﻿using Assets.Scripts.AI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager
    {
        public AIDirector AiDirector;
        private static GameManager _instance;
        private static List<PlayerCTRL> _mListOfPlayers;

        private GameManager()
        {
            AiDirector = new AIDirector();
            _mListOfPlayers = new List<PlayerCTRL>();
        }

        public static GameManager Instance()
        {
            return _instance ?? (_instance = new GameManager());
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
