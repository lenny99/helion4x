using Godot;
using Helion4x.Runtime;
using MonoCustomResourceRegistry;

namespace Helion4x.Gui
{
    [RegisteredType(nameof(FleetContextControl), baseType: nameof(PopupMenu))]
    public class FleetContextControl : ContextControl<Fleet>
    {
        private PopupMenu _navigation;

        public override void _Ready()
        {
            Connect(IdPressed, this, nameof(OnIndexPressed));
            _navigation = new PopupMenu();
            _navigation.Name = "Navigation";
            AddChild(_navigation);
            _navigation.Connect(IndexPressed, this, nameof(OnNavigationIndexPressed));
            AddSubmenuItem("Navigate", "Navigation", 0);
            _navigation.AddItem("Move to", 0);
            Popup_();
        }

        private void OnIndexPressed(int index)
        {
        }

        private void OnNavigationIndexPressed(int index)
        {
            if (index == 0)
                MoveTo();
        }

        private void MoveTo()
        {
            Collider.MatchSome(collider =>
            {
                if (collider is AstronomicalBody body)
                    Model.NavigateTo(body);
            });
            GetTree().SetInputAsHandled();
        }
    }
}