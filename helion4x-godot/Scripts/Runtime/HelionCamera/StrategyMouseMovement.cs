using Godot;

namespace Helion4x.Runtime.HelionCamera
{
    public class StrategyMouseMovement
    {
        private readonly StrategyCamera _strategyCamera;
        private readonly float _rotationSpeed;
        private readonly float _zoomSpeed;
        private Vector3 _dragCurrent;
        private Vector3 _dragStart;

        public StrategyMouseMovement(StrategyCamera strategyCamera, float zoomSpeed, float rotationSpeed)
        {
            _strategyCamera = strategyCamera;
            _zoomSpeed = zoomSpeed;
            _rotationSpeed = rotationSpeed;
        }

        // internal void HandleMouseMovement()
        // {
        //     if (!_cameraActions.Player.Drag.IsPressed()) return;
        //     _dragCurrent = RaycastUtils.GetPlanePosition();
        //     _camera.NewPosition = _camera.transform.position + _dragStart - _dragCurrent;
        // }
        //
        // private void HandleFirstDrag(InputAction.CallbackContext obj)
        // {
        //     _dragStart = RaycastUtils.GetPlanePosition();
        // }
        //
        // public void HandleFirstMouseRotation(InputAction.CallbackContext obj)
        // {
        //     _camera.RotateStart = Mouse.current.position.ReadValue();
        // }
        //
        // internal void HandleMouseRotation()
        // {
        //     if (_cameraActions.Player.DragRotate.IsPressed())
        //     {
        //         _camera.RotateCurrent = Mouse.current.position.ReadValue();
        //         var difference = _camera.RotateStart - _camera.RotateCurrent;
        //         _camera.RotateStart = _camera.RotateCurrent;
        //         _camera.NewRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
        //     }
        // }
        //
        internal void HandleMouseZoom()
        {

            var zoomDirection = _strategyCamera.Translation.DirectionTo(_strategyCamera.Translation);
            var zoom = Input.GetAxis("zoom_out", "zoom_in");
            if (zoom != 0)
            {
                var distanceFromRig = _strategyCamera.Translation.DistanceTo(_strategyCamera.Camera.Translation);
                var newZoom = _strategyCamera.NewZoom + zoomDirection * zoom * Mathf.Sqrt(distanceFromRig * _zoomSpeed);
                if (newZoom.y > 0.1 && newZoom.z < -0.1) _strategyCamera.NewZoom = newZoom;
            }
        }
        //
        // internal void HandleKeyboardRotation()
        // {
        //     var rotation = _cameraActions.Player.Rotate.ReadValue<float>();
        //     _camera.NewRotation *= Quaternion.Euler(Vector3.up * rotation * _rotationSpeed);
        // }
    }
}