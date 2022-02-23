using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using Helion4x.Core;
using Helion4x.Util;

namespace Helion4x.Runtime
{
    public class StrategyCamera : Spatial
    {
        private Spatial _focusSpatial;
        private float _rayLength;

        public Camera Camera { get; private set; }
        public Vector3 NewPosition { get; private set; }
        public Basis NewRotation { get; private set; }
        public Vector3 NewZoom { get; private set; }
        public Vector2 RotateStart { get; private set; }
        public Vector2 RotateCurrent { get; private set; }

        public override void _Ready()
        {
            NewPosition = GlobalTransform.origin;
            NewRotation = GlobalTransform.basis;
            Camera = GetNode<Camera>(_cameraPath);
            NewZoom = Camera.Translation;
            _rayLength = GetViewport().GetCamera().Far;
        }

        public override void _Input(InputEvent @event)
        {
            if (!(@event is InputEventMouseButton mouseEvent) || !mouseEvent.IsPressed()) return;
            switch (mouseEvent.ButtonIndex)
            {
                case (int) ButtonList.WheelUp:
                case (int) ButtonList.WheelDown:
                    Zoom(mouseEvent);
                    break;
                case (int) ButtonList.Left:
                {
                    if (mouseEvent.Doubleclick)
                        RaycastFocus(mouseEvent);
                    Select(mouseEvent);
                    Selection.CloseContext();
                    break;
                }
                case (int) ButtonList.Right:
                    Rotate(mouseEvent);
                    OpenContext(mouseEvent);
                    GetTree().SetInputAsHandled();
                    break;
            }
        }

        private void OpenContext(InputEventMouseButton mouseEvent)
        {
            var godotCollision = FireRaycastFromMouse(mouseEvent);
            var collision = new Collision(godotCollision);
            Selection.OpenContextMenu(mouseEvent, collision);
        }

        private void RaycastFocus(InputEventMouseButton mouseEvent)
        {
            var collision = FireRaycastFromMouse(mouseEvent);
            if (collision.Contains("collider")
                && collision["collider"] is IFollowable followable)
                _focusSpatial = followable.Followable;
            else
                _focusSpatial = null;
        }

        private void Rotate(InputEventMouseButton mouseEvent)
        {
            GetViewport().GetMousePosition();
            // TODO add command view to screen
        }

        private void Select(InputEventMouseButton inputEventMouseButton)
        {
            var collision = FireRaycastFromMouse(inputEventMouseButton);
            if (!collision.Contains("collider")) return;
            Selection.Select(collision["collider"]);
            GetTree().SetInputAsHandled();
        }

        private Dictionary FireRaycastFromMouse(InputEventMouseButton inputEventMouseButton)
        {
            var viewport = GetViewport();
            var from = Camera.ProjectRayOrigin(inputEventMouseButton.Position);
            var to = from + Camera.ProjectRayNormal(inputEventMouseButton.Position) * _rayLength;
            var collision = viewport.World.DirectSpaceState.IntersectRay(from, to);
            return collision;
        }

        private void Zoom(InputEventMouseButton mouseEvent)
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
            if (_focusSpatial != null)
            {
                NewPosition = _focusSpatial.GlobalTransform.origin;
                if (ShouldExitFollow()) _focusSpatial = null;
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

        #region Exports

        [Export] private NodePath _cameraPath;
        [Export] private readonly Vector3 _zoomVector = new Vector3(0, -10, 10);
        [Export] private readonly float _movementSpeed = 1;
        [Export] private readonly float _movementTime = 1;
        [Export] private readonly float _rotationSpeed = 1;
        [Export] private float _rotationTime = 1;
        [Export] private readonly float _zoomSpeed = 1;
        [Export] private readonly float _zoomTime = 1;

        #endregion
    }
}