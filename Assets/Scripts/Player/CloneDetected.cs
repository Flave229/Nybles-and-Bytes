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
            GameObject tempRef = gameObject;
            GameManager.Instance().GetListOfEntities().Remove(tempRef.GetComponent<PlayerCTRL>());
            MonoBehaviour.Destroy(tempRef, 0.0f);
        }

        public void Escaped()
        {

        }
    }
}
