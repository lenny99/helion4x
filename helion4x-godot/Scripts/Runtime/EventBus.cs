using System;
using Godot;

namespace Helion4x.Runtime
{
    public class EventBus : Node
    {
        public static event Action<Selectable> Selected = delegate { };
        public static event Action<Selectable> Unselected = delegate { };

        public static void Select(Selectable selectable)
        {
            Selected(selectable);
        }

        public static void Unselect(Selectable selectable)
        {
            Unselected(selectable);
        }
    }
}