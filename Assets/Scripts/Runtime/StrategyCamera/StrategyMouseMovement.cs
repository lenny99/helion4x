using Helion4x.Runtime.StrategyCamera;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime.Camera
{
    public class StrategyMouseMovement
    {
        private CloseUpPlayerCamera _closeUpPlayerCamera;
        private MyPlayerActions _cameraActions;
        private Vector3 _dragCurrent;
        private Vector3 _dragStart;

        public StrategyMouseMovement(CloseUpPlayerCamera closeUpPlayerCamera)
        {
            _closeUpPlayerCamera = closeUpPlayerCamera;
            _cameraActions = new MyPlayerActions();
        }

        internal void HandleMouseMovement()
        {
            if (!_cameraActions.Player.Drag.IsPressed()) return;
            _dragCurrent = CloseUpPlayerCamera.GetPlanePosition();
            _closeUpPlayerCamera.NewPosition = _closeUpPlayerCamera.transform.position + _dragStart - _dragCurrent;
        }

        public void HandleFirstMouseRotation(InputAction.CallbackContext obj)
        {
            _closeUpPlayerCamera._rotateStart = Mouse.current.position.ReadValue();
        }
        
        private void HandleFirstDrag(InputAction.CallbackContext obj)
        {
            StrategyMouseMovement._dragStart = GetPlanePosition();
        }

        internal void HandleMouseRotation()
        {
            if (_cameraActions.Player.DragRotate.IsPressed())
            {
                _closeUpPlayerCamera.RotateCurrent = Mouse.current.position.ReadValue();
                var difference = _closeUpPlayerCamera.RotateStart - _closeUpPlayerCamera.RotateCurrent;
                _closeUpPlayerCamera.RotateStart = _closeUpPlayerCamera.RotateCurrent;
                _closeUpPlayerCamera.NewRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
            }
        }

        internal void HandleMouseZoom()
        {
            var zoomDirection = Vector3.MoveTowards(_closeUpPlayerCamera.cameraTransform.localPosition, _closeUpPlayerCamera.transform.position, 1).normalized;
            var zoom = _cameraActions.Player.Zoom.ReadValue<float>() / 120;
            if (zoom != 0)
            {
                var distanceFromRig = Vector3.Distance(_closeUpPlayerCamera.transform.position, _closeUpPlayerCamera.cameraTransform.position);
                var newZoom = _closeUpPlayerCamera._newZoom + zoomDirection * zoom * Mathf.Sqrt(distanceFromRig * _closeUpPlayerCamera.zoomSpeed);
                if (newZoom.y > 0.1 && newZoom.z < -0.1) _closeUpPlayerCamera._newZoom = newZoom;
            }
        }

        internal void HandleKeyboardRotation()
        {
            var rotation = _cameraActions.Player.Rotate.ReadValue<float>();
            _closeUpPlayerCamera._newRotation *= Quaternion.Euler(Vector3.up * rotation * _closeUpPlayerCamera.rotationSpeed);
        }
    }
}