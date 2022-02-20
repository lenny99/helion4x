using System;
using Godot;
using Helion4x.Gui;
using Helion4x.Util;
using Environment = Helion4x.Core.Environment;

namespace Helion4x.Runtime
{
    public class Selection : Node
    {
        private ISelectable _currentSelectable;
        private VBoxContainer _selectionContainer;
        public static event Action<InputEventMouseButton> ContextOpend = delegate { };
        public static event Action ContextClosed = delegate { };

        private void OnSelected(object obj)
        {
            if (!(obj is ISelectable selectable)) return;
            _selectionContainer.ClearChildren();
            _currentSelectable = selectable;
            foreach (var selection in selectable.Select())
                if (selection is Settlement settlement)
                    OpenSettlement(settlement);
                else if (selection is Environment environment)
                    OpenEnvironment(environment);
                else if (selection is Fleet fleet) OpenFleet(fleet);
        }

        private void OpenSettlement(Settlement settlement)
        {
            var scene = _settlementView.Instance<SettlementSelectionControl>();
            scene.Settlement = settlement;
            _selectionContainer.AddChild(scene);
        }

        private void OpenEnvironment(Environment environment)
        {
            var scene = _environmentView.Instance<EnvironmentSelectionControl>();
            scene.Environment = environment;
            _selectionContainer.AddChild(scene);
        }

        private void OpenFleet(Fleet fleet)
        {
            var scene = _fleetControl.Instance<FleetControl>();
            scene.Fleet = fleet;
            _selectionContainer.AddChild(scene);
        }

        public override void _Ready()
        {
            _selectionContainer = GetNode<VBoxContainer>(_selectionContainerPath);
            Selected += OnSelected;
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
                _currentSelectable?.Unselect();
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

        public static event Action<object> Selected = delegate { };

        public static void Select(object selectable)
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