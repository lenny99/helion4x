using Godot;
using Optional;

namespace Helion4x.Runtime
{
    public struct Selectables
    {
        public Option<Environment> Environment { get; set; }
        public Option<Settlement> Settlement { get; set; }
        public Option<Fleet> Fleet { get; set; }
    }

    public class Selectable : StaticBody
    {
        public Selectables Select()
        {
            var selectables = new Selectables();
            if (GetParent() is Fleet fleet)
                selectables.Fleet = Option.Some(fleet);
            foreach (var child in GetParent().GetChildren())
            {
                if (child is Environment environment)
                    selectables.Environment = Option.Some(environment);
                if (child is Settlement settlement)
                    selectables.Settlement = Option.Some(settlement);
            }

            return selectables;
        }

        public void Unselect()
        {
        }
    }
}