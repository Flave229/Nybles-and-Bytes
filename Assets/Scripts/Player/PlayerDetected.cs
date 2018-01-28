using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    class PlayerDetected : IDetectable
    {
        public void Detected()
        {
            GameManager.Instance().GetSoundManager().StopMusic();
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Music/DyingSound", false);
            
            Scene current = SceneManager.GetActiveScene();
            PlayerPrefs.SetString("LastLevel",current.name.ToString());
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }

        public void Escaped()
        {

        }
    }
}
