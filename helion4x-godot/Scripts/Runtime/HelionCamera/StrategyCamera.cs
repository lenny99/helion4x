using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Helion4x.Core;

namespace Helion4x.Runtime.HelionCamera
{
    public class StrategyCamera : Spatial
    {
        private Spatial _followTransform;

        private ISelectable _selected;


        public Camera Camera { get; private set; }
        public Vector3 NewPosition { get; private set; }
        public Basis NewRotation { get; private set; }
        public Vector3 NewZoom { get; private set; }
        public Vector2 RotateStart { get; private set; }
        public Vector2 RotateCurrent { get; private set; }
        public Vector3 DragStart { get; }
        public Vector3 DragCurrent { get; }

        public override void _Ready()
        {
            NewPosition = GlobalTransform.origin;
            NewRotation = GlobalTransform.basis;
            Camera = GetNode<Camera>(_cameraPath);
            NewZoom = Camera.Translation;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.IsPressed()) HandleZoomInput(mouseEvent);
        }

        private void HandleZoomInput(InputEventMouseButton mouseEvent)
        {
            var zoom = GetZoom(mouseEvent);
            if (zoom == 0) return;
            var distanceFromRig = Translation.DistanceTo(Camera.Translation);
            var newZoom = NewZoom + _zoomVector * zoom * Mathf.Sqrt(distanceFromRig * _zoomSpeed);
            if (newZoom.y > 0.1 && newZoom.z < -0.1) NewZoom = newZoom;
        }

        private static int GetZoom(InputEventMouseButton mouseEvent)
        {
            var zoom = 0;
            switch (mouseEvent.ButtonIndex)
            {
                case (int) ButtonList.WheelUp:
                    zoom += 1;
                    break;
                case (int) ButtonList.WheelDown:
                    zoom -= 1;
                    break;
            }

            return zoom;
        }

        public override void _Process(float delta)
        {
            if (_followTransform != null)
            {
                NewPosition = _followTransform.GlobalTransform.origin;
                if (ShouldExitFollow()) _followTransform = null;
            }
            else
            {
                HandleMouseMovement(delta);
            }

            HandleMouseRotation(delta);
            HandleKeyboardMovement(delta);
            HandleKeyboardRotation(delta);

            Translation = Translation.Lerp(NewPosition, _movementTime);
            Camera.Translation = Camera.Translation.Lerp(NewZoom, _zoomTime);
            Rotation = NewRotation.GetEuler();
        }

        private void HandleMouseRotation(float delta)
        {
            if (Input.IsActionJustPressed("right_click")) RotateStart = GetViewport().GetMousePosition();

            if (Input.IsActionPressed("right_click"))
            {
                RotateCurrent = GetViewport().GetMousePosition();
                var difference = RotateStart - RotateCurrent;
                RotateStart = RotateCurrent;
                NewRotation *= new Basis(Vector3.Up * difference.x * delta);
            }
        }

        private bool ShouldExitFollow()
        {
            return new List<string> {"forward", "backward", "left", "right"}
                .Select(input => Input.IsActionPressed(input))
                .Contains(true);
        }

        private void HandleMouseMovement(float delta)
        {
            // if (!_cameraActions.Player.Drag.IsPressed()) return;
            // _dragCurrent = this.GetPlanePosition();
            // _camera.NewPosition = _camera.transform.position + _dragStart - _dragCurrent;
        }

        private void HandleKeyboardMovement(float delta)
        {
            float forwardBackward = 0;
            float leftRight = 0;
            if (Input.IsActionPressed("forward")) forwardBackward += 1;

            if (Input.IsActionPressed("backward")) forwardBackward -= 1;

            forwardBackward *= _movementSpeed;

            if (Input.IsActionPressed("left")) leftRight += 1;

            if (Input.IsActionPressed("right")) leftRight -= 1;

            leftRight *= _movementSpeed;

            NewPosition += NewRotation.Xform(new Vector3(leftRight, 0, forwardBackward)) * delta;
        }

        private void HandleKeyboardRotation(float delta)
        {
            var rotation = Input.GetAxis("rotate_right", "rotate_left");
            NewRotation = NewRotation.Rotated(Vector3.Up, rotation * _rotationSpeed * delta);
        }

        public static event Action<ISelectable> Selected = delegate { };
        public static event Action<ISelectable> Unselected = delegate { };

        #region Exports

        [Export] private NodePath _cameraPath;
        [Export] private Vector3 _zoomVector = new Vector3(0, -10, 10);
        [Export] private float _movementSpeed = 1;
        [Export] private float _movementTime = 1;
        [Export] private float _rotationSpeed = 1;
        [Export] private float _rotationTime = 1;
        [Export] private float _zoomSpeed = 1;
        [Export] private float _zoomTime = 1;

        #endregion


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
}