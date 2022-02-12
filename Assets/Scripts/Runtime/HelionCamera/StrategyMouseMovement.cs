using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime.HelionCamera
{
    public class StrategyMouseMovement
    {
        private readonly StrategyCamera _camera;
        private readonly MyPlayerActions _cameraActions;
        private readonly float _rotationSpeed;
        private readonly float _zoomSpeed;
        private Vector3 _dragCurrent;
        private Vector3 _dragStart;

        public StrategyMouseMovement(StrategyCamera camera, float zoomSpeed, float rotationSpeed)
        {
            _camera = camera;
            _zoomSpeed = zoomSpeed;
            _rotationSpeed = rotationSpeed;
            _cameraActions = new MyPlayerActions();
            _cameraActions.Enable();
            _cameraActions.Player.Drag.started += HandleFirstDrag;
            _cameraActions.Player.DragRotate.started += HandleFirstMouseRotation;
        }

        internal void HandleMouseMovement()
        {
            if (!_cameraActions.Player.Drag.IsPressed()) return;
            _dragCurrent = RaycastUtils.GetPlanePosition();
            _camera.NewPosition = _camera.transform.position + _dragStart - _dragCurrent;
        }

        private void HandleFirstDrag(InputAction.CallbackContext obj)
        {
            _dragStart = RaycastUtils.GetPlanePosition();
        }

        public void HandleFirstMouseRotation(InputAction.CallbackContext obj)
        {
            _camera.RotateStart = Mouse.current.position.ReadValue();
        }

        internal void HandleMouseRotation()
        {
            if (_cameraActions.Player.DragRotate.IsPressed())
            {
                _camera.RotateCurrent = Mouse.current.position.ReadValue();
                var difference = _camera.RotateStart - _camera.RotateCurrent;
                _camera.RotateStart = _camera.RotateCurrent;
                _camera.NewRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
            }
        }

        internal void HandleMouseZoom()
        {
            var zoomDirection = Vector3.MoveTowards(_camera.CameraTransform.localPosition,
                _camera.transform.position, 1).normalized;
            var zoom = _cameraActions.Player.Zoom.ReadValue<float>() / 120;
            if (zoom != 0)
            {
                var distanceFromRig = Vector3.Distance(_camera.transform.position,
                    _camera.CameraTransform.position);
                var newZoom = _camera.NewZoom +
                              zoomDirection * zoom * Mathf.Sqrt(distanceFromRig * _zoomSpeed);
                if (newZoom.y > 0.1 && newZoom.z < -0.1) _camera.NewZoom = newZoom;
            }
        }

        internal void HandleKeyboardRotation()
        {
            var rotation = _cameraActions.Player.Rotate.ReadValue<float>();
            _camera.NewRotation *= Quaternion.Euler(Vector3.up * rotation * _rotationSpeed);
        }
    }
}