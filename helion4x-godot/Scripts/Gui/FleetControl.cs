using Godot;
using Helion4x.Runtime;

namespace Helion4x.Gui
{
    public class FleetControl : PanelContainer
    {
        private LineEdit _acceleration;

        private LineEdit _ships;
        public Fleet Fleet { get; set; }

        public override void _Ready()
        {
            _ships = GetNode<LineEdit>(_shipsPath);
            _acceleration = GetNode<LineEdit>(_accelerationPath);
        }

        public override void _Process(float delta)
        {
            if (Fleet == null) return;
            _ships.Text = Fleet.Ships.Count.ToString();
            _acceleration.Text = Fleet.Acceleration.ToString();
        }

        #region Exports

        [Export] private NodePath _shipsPath;
        [Export] private NodePath _accelerationPath;

        #endregion
    }
}