using Godot.Collections;
using Optional;

namespace Helion4x.Runtime
{
    public class Collision
    {
        private readonly Dictionary _collision;

        public Collision(Dictionary collision)
        {
            _collision = collision;
        }

        public Option<object> Collider => _collision.Contains("collider")
            ? Option.Some(_collision["collider"])
            : Option.None<object>();
    }
}