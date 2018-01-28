using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.GameLogic
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource _stealthBGM;
        public AudioSource _alertBGM;

        private SoundManager()
        {
        }

        public void Awake()
        {
            _stealthBGM.Stop();
            _alertBGM.Stop();
            DontDestroyOnLoad(this);
        }

        public void StartStealthMusic()
        {
            if (!_stealthBGM.isPlaying)
            {
                _stealthBGM.Play();
            }
        }

        public void StartAlertMusic()
        {
            _stealthBGM.Stop();

            if(!_alertBGM.isPlaying)
                _alertBGM.Play();
        }

        public void StopAlertMusic()
        {
            _alertBGM.Stop();
            StartStealthMusic();
        }

        public void PlaySoundEffect(string sfxFilename)
        {

        }
    }
}
