using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IMovementAI
    {
        List<Node> CreatePath(Vector3 source, Vector3 target);
    }
}