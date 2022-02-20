using System.Collections.Generic;
using Godot;

namespace Helion4x.Runtime
{
    public interface ISelectable
    {
        IEnumerable<Node> Select();
        void Unselect();
    }
}