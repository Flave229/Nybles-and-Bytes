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
            DoorNodes = Object.FindObjectsOfType<UnityAINode>().Select(x => x.AINode).OfType<Node>().ToList();
        }

        public void Update()
        {

        }
    }
}
