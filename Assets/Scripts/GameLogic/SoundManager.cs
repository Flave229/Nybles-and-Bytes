using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.GameLogic
{
    class SoundManager
    {
        private static SoundManager _instance;
        private AudioSource _stealthBGM;
        private AudioSource _alertBGM;

        private SoundManager()
        {

        }

        public static SoundManager Instance()
        {
            return _instance ?? (_instance = new SoundManager());
        }

        public void InitialiseMusic()
        {

        }

        public void StartStealthMusic()
        { 

        }

        public void StartAlertMusic()
        {

        }

        public void StopAlertMusic()
        {

        }

        public void PlaySoundEffect(string sfxFilename)
        {

        }
    }
}
