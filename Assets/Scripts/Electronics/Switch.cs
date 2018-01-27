using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Switch : MonoBehaviour, ICircuitComponent
    {
        public bool Enabled;
        public List<GameObject> PrevGameObjects;
        public List<GameObject> NextGameObjects;

        List<ICircuitComponent> PrevCircuitComponents = new List<ICircuitComponent>();
        List<ICircuitComponent> NextCircuitComponents = new List<ICircuitComponent>();

        public void Execute()
        {
            if (Enabled == false)
                return;

            foreach(ICircuitComponent connectedComponent in NextCircuitComponents)
            {
                connectedComponent.Execute();
            }
        }

        public List<ICircuitComponent> Peek()
        {
            if (Enabled == false)
                return new List<ICircuitComponent>();

            List<ICircuitComponent> components = new List<ICircuitComponent>();
            foreach(ICircuitComponent connectedComponents in NextCircuitComponents)
            {
                components.AddRange(connectedComponents.Peek());
            }
            return components;
        }

        public List<ICircuitComponent> SeekNext()
        {
            return NextCircuitComponents;
        }

        public List<ICircuitComponent> SeekPrev()
        {
            return PrevCircuitComponents;
        }
    }
}
