using Godot;
using Helion4x.Runtime;

namespace Helion4x.Util
{
    public static class SingletonExtensions
    {
        public static Player GetPlayer(this Node node)
        {
            return node.GetNode<Player>("/root/Player");
        }
    }
}