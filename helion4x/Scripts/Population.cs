using Godot;

namespace Helion4x.Scripts
{
    public class Population : Node
    {
        private float _population = 0;
        private float _carryCapacity = 0;
        private float _growthRate = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }

        public void GrowPopulation()
        {
            var K = _carryCapacity;
            var A = K - _population / _population;
            _population = K / 1 + A * Mathf.Pow(Mathf.E, _growthRate * 1);
        }

        public float GetPopulation()
        {
            return _population;
        }
    }
}
