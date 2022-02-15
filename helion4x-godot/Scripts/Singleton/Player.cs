using System;
using Godot;

namespace Helion4x.Runtime
{
    public class Player : Node
    {
        private Selectable _selectable;

        public Selectable Selectable
        {
            get => _selectable;
            set
            {
                if (value != null)
                    value.Select();
                else
                    _selectable?.Unselect();
                Selected(value);
                _selectable = value;
            }
        }

        public event Action<Selectable> Selected = delegate { };
    }
}