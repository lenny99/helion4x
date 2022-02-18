using Godot;
using Helion4x.Gui;
using Helion4x.Singleton;
using Helion4x.Util;

namespace Helion4x.Runtime
{
    public class SelectionSystem : Node
    {
        private Selectable _current;
        private VBoxContainer _selectionContainer;

        private void ShowSelectables(Selectable selectable)
        {
            if (_current != null && _current.Equals(selectable)) return;
            _current = selectable;
            selectable.Select();
            selectable.Settlement.MatchSome(settlement =>
            {
                var scene = _settlementView.Instance<SettlementSelectionControl>();
                scene.Settlement = settlement;
                _selectionContainer.AddChild(scene);
            });
            selectable.Environment.MatchSome(environment =>
            {
                var scene = _environmentView.Instance<EnvironmentSelectionControl>();
                scene.Environment = environment;
                _selectionContainer.AddChild(scene);
            });
        }

        public override void _Ready()
        {
            _selectionContainer = GetNode<VBoxContainer>(_selectionContainerPath);
            EventBus.Selected += ShowSelectables;
            EventBus.Unselected += delegate { _selectionContainer.ClearChildren(); };
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton inputEventMouseButton
                && inputEventMouseButton.IsPressed()
                && inputEventMouseButton.ButtonIndex == (int) ButtonList.Left)
            {
                _current = null;
                EventBus.InvokeUnselected();
            }
        }


        #region Exports

        [Export] private NodePath _selectionContainerPath;
        [Export] private PackedScene _settlementView;
        [Export] private PackedScene _environmentView;

        #endregion
    }
}