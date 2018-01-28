using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class PlayerDetected : IDetectable
    {
        public void Detected()
        {
            GameManager.Instance().GetSoundManager().StopMusic();
            GameManager.Instance().GetSoundManager().PlaySoundEffect("Music/DyingSound1", false);
            Scenes.instance.LoadScene(Scenes.Scene.GAME_OVER);
        }

        public void Escaped()
        {
            GameManager.Instance().GetSoundManager().StopAlertMusic();
        }
    }
}
