using Godot;
using UnitsNet;

namespace Helion4x.Runtime
{
    public class SpaceShip : Spatial
    {
        #region Exports

        [Export] private float _acceleration;

        #endregion

        public Acceleration Acceleration { get; private set; }

        public override void _Ready()
        {
            Acceleration = Acceleration.FromMetersPerSecondSquared(_acceleration);
        }
    }
}