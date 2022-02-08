using Helion4x.Core;
using Helion4x.Scripts;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class AstronomicalObject : MonoBehaviour, IAstronomicalObject
    {
        [SerializeField] private MassType massType;
        [SerializeField] private GameObject parent;
        private CircularOrbiter _circularOrbiter;
        private IAstronomicalObject _parent;

        public float Mass { get; private set; }

        private void Start()
        {
            Mass = AstronomicalMass.ForMassType(massType);
            TimeManager.MinutePassed += OnMinutePassed;
            if (parent == null) return;
            _parent = parent.GetComponent<IAstronomicalObject>();
            if (_parent == null) return;
            var radius = AstronomicalLength.FromMegameters(Vector3.Distance(_parent.transform.position, transform.position));
            var orbitalPeriod = new OrbitalPeriod(radius.Meters, _parent.Mass, OrbitType.Circular);
            _circularOrbiter = new CircularOrbiter(_parent, orbitalPeriod, radius);
        }

        private void OnMinutePassed()
        {
            if (_parent != null) transform.position = _circularOrbiter.CalculateNextPosition(1);
        }
    }
}