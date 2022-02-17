using System;
using Godot;
using Helion4x.Runtime;

namespace Helion4x.Singleton
{
    public class EventBus : Node
    {
        public static event Action<Selectable> Selected = delegate { };
        public static event Action Unselected = delegate { };

        public static void InvokeSelected(Selectable selectable)
        {
            Selected.Invoke(selectable);
        }


        public static void InvokeUnselected()
        {
            Unselected.Invoke();
        }
    }
}