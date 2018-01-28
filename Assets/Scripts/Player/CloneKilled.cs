using Assets.Scripts.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class CloneKilled : IEntityKilled
    {
        public void Killed()
        {
            UnityEngine.Object.FindObjectOfType<PickupUSB>().DropItem();
        }
    }
}
