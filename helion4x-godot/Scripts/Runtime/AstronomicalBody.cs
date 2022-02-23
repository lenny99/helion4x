using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Helion4x.Core;
using Helion4x.Scripts;
using Environment = Helion4x.Core.Environment;

namespace Helion4x.Runtime
{
    public class AstronomicalBody : Spatial, IAstronomicalBody, IFollowable, ISelectable
    {
        private CircularOrbit _circularOrbit;
        private IAstronomicalBody _parent;

        public PackedScene Context => null;

        public float Mass { get; private set; }

        public Spatial Followable => this;

        public IEnumerable<Node> Select()
        {
            GetNodeOrNull<Sprite3D>("Selection")?.Show();
            return GetChildren().Cast<Node>().Where(child => child is Settlement || child is Environment);
        }

        public void Unselect()
        {
            GetNodeOrNull<Sprite3D>("Selection")?.Hide();
        }

        public Node CreateContext(InputEventMouseButton mouseEvent, Collision collision)
        {
            throw new NotImplementedException();
        }

        public override void _Ready()
        {
            Mass = AstronomicalMass.ForMassType(MassType);
            TimeManager.MinutePassed += OnMinutePassed;
            if (_parentPath == null) return;
            _parent = GetNode<IAstronomicalBody>(_parentPath);
            var radius =
                AstronomicalLength.FromKilometers(_parent.Translation.DistanceTo(Translation));
            _circularOrbit = new CircularOrbit(_parent, radius);
        }

        private void OnMinutePassed()
        {
            Transform = Transform.Rotated(Vector3.Up, _rotation);
            if (_parent == null) return;
            Translation = _circularOrbit.CalculateNextPosition(1);
        }


        #region Exports

        [Export] public MassType MassType;
        [Export] private NodePath _parentPath;
        [Export] private float _rotation;

        #endregion
    }
}