using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IMovementAI
    {
        List<Node> CreatePath(Node source, Node target);
    }
}