using Godot;

namespace Helion4x.Runtime
{
    public class OrbitPath : Path
    {
        private Fleet _orbit;

        #region Exports

        [Export] private NodePath _orbitPath;

        #endregion

        public override void _Ready()
        {
            _orbit = GetNode<Fleet>(_orbitPath);
            var positions = _orbit.Orbit.GetPositions(360);
            foreach (var position in positions) Curve.AddPoint(position);
        }
    }
}