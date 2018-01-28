using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class CloneDetected : IDetectable
    {
        GameObject gameObject;

        public CloneDetected(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void Detected()
        {
            gameObject.GetComponent<BloodDeath>().Bleed();
            PickupUSB dataPacket = GameObject.Find("DataPacket").GetComponent<PickupUSB>();
            if (dataPacket.Holder != null && dataPacket.Holder == gameObject)
                dataPacket.DropItem();

            GameObject tempRef = gameObject;
            GameManager.Instance().GetListOfEntities().Remove(tempRef.GetComponent<PlayerCTRL>());
            int indexOfThisEntity = GameManager.Instance().GetListOfEntities().IndexOf(tempRef.GetComponent<PlayerCTRL>());
            UnityEngine.Object.Destroy(tempRef, 0.0f);
        }

        public void Escaped()
        {
            GameManager.Instance().GetSoundManager().StopAlertMusic();
        }
    }
}
