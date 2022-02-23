using Godot;
using Optional;

namespace Helion4x.Gui
{
    public abstract class ContextControl<T> : PopupMenu
    {
        protected const string IndexPressed = "index_pressed";
        protected const string IdPressed = "id_pressed";
        public T Model { get; set; }
        public Option<object> Collider { get; set; }
    }
}