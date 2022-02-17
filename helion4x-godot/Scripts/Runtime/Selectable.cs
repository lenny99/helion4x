using Godot;
using Optional;

namespace Helion4x.Runtime
{
    public class Selectable : StaticBody, ISelectable
    {
        public Option<Environment> Environment { get; private set; }
        public Option<Settlement> Settlement { get; private set; }

        public void Select()
        {
            foreach (var child in GetParent().GetChildren())
            {
                if (child is Environment environment)
                    Environment = Option.Some(environment);
                if (child is Settlement settlement)
                    Settlement = Option.Some(settlement);
            }
        }

        public override void _Ready()
        {
            Environment = Option.None<Environment>();
            Settlement = Option.None<Settlement>();
        }

        public void Unselect()
        {
        }
    }
}