using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AIDirector
    {
        public List<Node> DoorNodes;

        public AIDirector()
        {
            DoorNodes = Object.FindObjectsOfType<Lift>().Select(x => x.PathfindNode).OfType<Node>().ToList();
        }

        public void Update()
        {
            if (DoorNodes.Count <= 0)
                DoorNodes = Object.FindObjectsOfType<Lift>().Select(x => x.PathfindNode).OfType<Node>().ToList();
        }
    }
}
