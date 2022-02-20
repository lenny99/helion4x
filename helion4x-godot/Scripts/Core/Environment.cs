using Godot;

namespace Helion4x.Core
{
    public class Environment : Node
    {
        public Environment(float temperature)
        {
            Temperature = temperature;
        }

        public float Temperature { get; }
    }
}