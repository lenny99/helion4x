using System.Collections.Generic;
using System.Linq;
using Godot;
using Helion4x.Core;
using Helion4x.Gui;
using UnitsNet;
using UnitsNet.Units;

namespace Helion4x.Runtime
{
    public class Fleet : Spatial, IFollowable, ISelectable
    {
        private MovementType _movement;
        private IAstronomicalBody _parent;
        private Speed _speed;
        private Voyage _voyage;
        public CircularOrbit Orbit;

        public List<SpaceShip> Ships => GetNode("Ships").GetChildren().Cast<SpaceShip>().ToList();

        public Acceleration Acceleration => Ships.Count > 0
            ? Ships.Select(ship => ship.Acceleration).Min(AccelerationUnit.MeterPerSecondSquared)
            : Acceleration.Zero;

        public Spatial Followable => this;

        public IEnumerable<Node> Select()
        {
            GetNode<Sprite3D>("Selection")?.Show();
            return new[] {this};
        }

        public void Unselect()
        {
            GetNode<Sprite3D>("Selection")?.Hide();
        }

        public Node CreateContext(InputEventMouseButton mouseEvent, Collision collision)
        {
            var context = new FleetContextControl();
            context.RectPosition = mouseEvent.Position;
            context.Collider = collision.Collider;
            context.Model = this;
            var control = new Control();
            control.AddChild(context);
            return control;
        }

        public override void _Ready()
        {
            TimeManager.MinutePassed += OnMinutePassed;
            _parent = GetNode<IAstronomicalBody>(_parentPath);
            _movement = MovementType.Orbit;
            if (_parent == null) return;
            var radius =
                AstronomicalLength.FromKilometers(_parent.Translation.DistanceTo(Translation));
            Orbit = new CircularOrbit(_parent, radius);
        }

        private void OnMinutePassed()
        {
            var newSpeed = _speed.KilometersPerSecond +
                           Acceleration.KilometersPerSecondSquared;
            _speed = Speed.FromKilometersPerMinutes(newSpeed);
            if (_movement == MovementType.Direct) Translation = _voyage.NextPosition(1, _speed);
            if (_movement == MovementType.Orbit) Translation = Orbit.CalculateNextPosition(1);
        }

        public void NavigateTo(AstronomicalBody body)
        {
            _voyage = new Voyage(Translation, body.Translation);
            _movement = MovementType.Direct;
        }

        #region Exports

        [Export] private NodePath _parentPath;
        [Export] private float _movementTime;
        [Export] private int _orbitSegments;

        #endregion
    }

    internal enum MovementType
    {
        Direct,
        Orbit
    }
}