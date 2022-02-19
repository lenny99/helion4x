using System;
using Godot;
using Helion4x.Gui;
using Helion4x.Util;

namespace Helion4x.Runtime
{
    public class Selection : Node
    {
        private Selectable _currentSelectable;
        private VBoxContainer _selectionContainer;
        public static event Action<InputEventMouseButton> ContextOpend = delegate { };
        public static event Action ContextClosed = delegate { };

        private void ShowSelectables(Selectable selectable)
        {
            if (_currentSelectable != null && _currentSelectable.Equals(selectable)) return;
            _currentSelectable = selectable;
            var selectables = selectable.Select();
            selectables.Settlement.MatchSome(settlement =>
            {
                var scene = _settlementView.Instance<SettlementSelectionControl>();
                scene.Settlement = settlement;
                _selectionContainer.AddChild(scene);
            });
            selectables.Environment.MatchSome(environment =>
            {
                var scene = _environmentView.Instance<EnvironmentSelectionControl>();
                scene.Environment = environment;
                _selectionContainer.AddChild(scene);
            });
            selectables.Fleet.MatchSome(fleet =>
            {
                var scene = _fleetControl.Instance<FleetControl>();
                scene.Fleet = fleet;
                _selectionContainer.AddChild(scene);
            });
        }

        public override void _Ready()
        {
            _selectionContainer = GetNode<VBoxContainer>(_selectionContainerPath);
            Selected += ShowSelectables;
            ContextOpend += OnContextOpen;
            ContextClosed += OnContextClosed;
        }

        private void OnContextClosed()
        {
            foreach (Node child in GetChildren())
                if (child is ContextControl contextControl)
                    RemoveChild(contextControl);
        }

        private void OnContextOpen(InputEventMouseButton obj)
        {
            if (_currentSelectable == null) return;
            var contextControl = _contextControl.Instance<ContextControl>();
            contextControl.RectPosition = obj.Position;
            AddChild(contextControl);
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton inputEventMouseButton
                && inputEventMouseButton.IsPressed()
                && inputEventMouseButton.ButtonIndex == (int) ButtonList.Left)
            {
                _currentSelectable = null;
                _selectionContainer.ClearChildren();
            }
        }

        public static void OpenContextMenu(InputEventMouseButton inputEventMouseButton)
        {
            ContextOpend(inputEventMouseButton);
        }

        public static void CloseContext()
        {
            ContextClosed();
        }

        public static event Action<Selectable> Selected = delegate { };

        public static void Select(Selectable selectable)
        {
            Selected.Invoke(selectable);
        }

        #region Exports

        [Export] private NodePath _selectionContainerPath;
        [Export] private PackedScene _settlementView;
        [Export] private PackedScene _environmentView;
        [Export] private PackedScene _fleetControl;
        [Export] private PackedScene _contextControl;

        #endregion
    }
}