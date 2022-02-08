using UnityEngine;
using UnitsNet;

namespace Helion4x.Runtime
{
    public class Voyage
    {
        private readonly Vector3 _destination;
        private Vector3 _origin;
        private Vector3 _position;

        public Voyage(Vector3 origin, Vector3 destination)
        {
            _origin = origin;
            _destination = destination;
            _position = _origin;
        }

        public Vector3 NextPosition(int minutes, Speed speed)
        {
            var direction = Vector3.MoveTowards(_origin, _destination, 1f);
            var movement = direction * (float) speed.KilometersPerMinutes * minutes;
            _position += movement;
            return _position;
        }
    }
}