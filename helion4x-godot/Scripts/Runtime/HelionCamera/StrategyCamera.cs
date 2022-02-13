using System;
using System.Security.AccessControl;
using Godot;
using Helion4x.Core;

namespace Helion4x.Runtime.HelionCamera
{
    public class StrategyCamera : Spatial
    {
        [Export] private NodePath _cameraPath;
        [Export] private float _movementSpeed = 1;
        [Export] private float _movementTime = 1;
        [Export] private float _rotationSpeed = 1;
        [Export] private float _rotationTime = 1;
        [Export] private float _zoomSpeed = 1;
        [Export] private float _zoomTime = 1;

        private Transform? _followTransform;
        private ISelectable _selected;
        private StrategyMouseMovement _strategyMouseMovement;

        public Camera Camera { get; internal set; }
        public Vector3 NewPosition { get; internal set; }
        public Transform NewRotation { get; internal set; }
        public Vector3 NewZoom { get; internal set; }
        public Vector3 RotateCurrent { get; internal set; }
        public Vector3 RotateStart { get; internal set; }

        public override void _Ready()
        {
            NewPosition = Translation;
            NewRotation = Transform;
            Camera = GetNode<Camera>(_cameraPath);
            NewZoom = Camera.Translation;
            _strategyMouseMovement = new StrategyMouseMovement(this, _zoomSpeed, _rotationSpeed);
        }

        public override void _Process(float delta)
        {
            // if (_followTransform != null)
            // {
            //     NewPosition = _followTransform.position;
            //     if (IsExitingFollow()) _followTransform = null;
            // }
            // else
            // {
            //     _strategyMouseMovement.HandleMouseMovement();
            // }
            HandleKeyboardMovement(delta);
            HandleKeyboardRotation(delta);
            // _strategyMouseMovement.HandleMowuseRotation();
            _strategyMouseMovement.HandleMouseZoom();

            Translation =
                Translation.Lerp(NewPosition, delta);
            Camera.Translation =
                Camera.Translation.Lerp(NewZoom, delta);
            Transform = NewRotation;
        }

        private void HandleKeyboardMovement(float delta)
        {
            float forwardBackward = 0;
            float leftRight = 0;
            if (Input.IsActionPressed("forward"))
            {
                forwardBackward += 1;
            }

            if (Input.IsActionPressed("backward"))
            {
                forwardBackward -= 1;
            }

            forwardBackward *= _movementSpeed;

            if (Input.IsActionPressed("left"))
            {
                leftRight += 1;
            }

            if (Input.IsActionPressed("right"))
            {
                leftRight -= 1;
            }

            leftRight *= _movementSpeed;

            NewPosition += new Vector3(leftRight, 0, forwardBackward);
        }

        private void HandleKeyboardRotation(float delta)
        {
            float rotation = 0;
            if (Input.IsActionPressed("rotate_left")) rotation = -1;
            if (Input.IsActionPressed("rotate_right")) rotation = 1;
            NewRotation = Transform.Rotated(Vector3.Up, rotation * _rotationSpeed);
        }

        public static event Action<ISelectable> Selected = delegate { };
        public static event Action<ISelectable> Unselected = delegate { };


        // private void HandleClick(InputAction.CallbackContext obj)
        // {
        //     GetWorld().Space.
        //     var ray = Godot.Camera.main!.ScreenPointToRay(Mouse.current.position.ReadValue());
        //     if (Physics.Raycast(ray, out var hit))
        //     {
        //         var selectable = hit.collider.GetComponent<ISelectable>();
        //         selectable?.Select();
        //         _selected = selectable;
        //         Selected.Invoke(_selected);
        //     }
        //     else if (_selected != null)
        //     {
        //         _selected.Unselect();
        //         Unselected.Invoke(_selected);
        //         _selected = null;
        //     }
        // }
    }
    
    static class Vector3Extensions
    {
        public static Vector3 Lerp(this Vector3 from, Vector3 to, float weight)
        {
            var x= Mathf.Lerp(from.x, to.x, weight);
            var y = Mathf.Lerp(from.y, to.y, weight);
            var z = Mathf.Lerp(from.z, to.z, weight);
            return new Vector3(x, y, z);
        }
    }
}