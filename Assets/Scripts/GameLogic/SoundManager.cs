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
        public AudioSource _sfxSource;

        public AudioClip[] WalkSFX;
        public AudioSource WalkSFXSource;

        private float _waitBeforeNextSound;

        private SoundManager()
        {
        }

        public void Awake()
        {
            _waitBeforeNextSound = 2.0f;
            _stealthBGM.Stop();
            _alertBGM.Stop();
            DontDestroyOnLoad(this);
        }

        public void StartStealthMusic()
        {
            _alertBGM.Stop();
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

        public void StopMusic()
        {
            _stealthBGM.Stop();
            _alertBGM.Stop();
        }

        public void StopAlertMusic()
        {
            _alertBGM.Stop();
            StartStealthMusic();
        }

        public void PlaySoundEffect(string sfxFilename, bool loop)
        {
            _sfxSource.clip = (AudioClip)Resources.Load(sfxFilename, typeof(AudioClip));
            _sfxSource.loop = loop;
            _sfxSource.Play();
        }

        public void PlayWalkSFX()
        {
            int randSFX = UnityEngine.Random.Range(0, WalkSFX.Length);
            WalkSFXSource.clip = WalkSFX[randSFX];
         
            if(!WalkSFXSource.isPlaying && _waitBeforeNextSound <= 0.0f)
            {
                WalkSFXSource.Play();
                _waitBeforeNextSound = 2.0f;
            }
            else
            {
                if (_waitBeforeNextSound > 0.0f)
                    _waitBeforeNextSound -= 0.02f;
            }
        }
    }
}
