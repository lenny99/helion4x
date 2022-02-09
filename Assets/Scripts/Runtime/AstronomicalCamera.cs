using System.Net.Sockets;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Helion4x.Runtime
{
    public class AstronomicalCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField, Range(0, 1)] private float movementSpeed;
        [SerializeField, Range(0, 10)] private float movementTime;
        [SerializeField, Range(0, 1)] private float rotationSpeed;
        [SerializeField, Range(0, 10)] private float rotationTime;
        [SerializeField, Range(0, 5)] private float zoomSpeed;
        [SerializeField, Range(0, 10)] private float zoomTime;

        private Vector3 _newPosition;
        private Quaternion _newRotation;
        private Vector3 _newZoom;

        private Vector3 _dragStart;
        private Vector3 _dragCurrent;
        private Vector3 _rotateStart;
        private Vector3 _rotateCurrent;

        private @CameraActions _cameraActions;

        private Transform _followTransform;

        private void Awake()
        {
            _cameraActions = new CameraActions();
            _cameraActions.Player.Enable();
            _cameraActions.Player.Drag.performed += HandleFirstDrag;
            _cameraActions.Player.DragRotate.performed += HandleFirstMouseRotation;
        }

        private void Start()
        {
            _newPosition = transform.position;
            _newRotation = transform.rotation;
            _newZoom = cameraTransform.localPosition;
            cameraTransform.rotation.SetLookRotation(transform.position);
        }

        private void Update()
        {
            if (_cameraActions.Player.Click.IsPressed())
            {
                SelectFollow();
            }
            if (_followTransform != null)
            {
                _newPosition = _followTransform.position;
                if (_cameraActions.Player.Move.IsPressed()) _followTransform = null;
            }
            else
            {
                HandleKeyboardMovement();
                HandleMouseMovement();
            }
            HandleKeyboardRotation();
            HandleMouseRotation();
            HandleMouseZoom();
            transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * rotationTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * zoomTime);
        }

        private void SelectFollow()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var ray = Camera.main!.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var followable = hit.collider.GetComponent<IFollowable>();
            _followTransform = followable.Transform;
        }

        private void HandleFirstDrag(InputAction.CallbackContext obj)
        {
            _dragStart = GetPlanePosition();
        }

        private void HandleMouseMovement()
        {
            if (!_cameraActions.Player.Drag.IsPressed()) return;
            _dragCurrent = GetPlanePosition();
            _newPosition = transform.position + _dragStart - _dragCurrent;
        }

        private void HandleFirstMouseRotation(InputAction.CallbackContext obj)
        {
            _rotateStart = Mouse.current.position.ReadValue();
        }

        private void HandleMouseRotation()
        {
            if (_cameraActions.Player.DragRotate.IsPressed())
            {
                _rotateCurrent = Mouse.current.position.ReadValue();
                Vector3 difference = _rotateStart - _rotateCurrent;
                _rotateStart = _rotateCurrent;
                _newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
            }
        }

        private static Vector3 GetPlanePosition()
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main!.ScreenToWorldPoint(mousePosition);
            var ray = Camera.main!.ScreenPointToRay(mouseWorldPosition);
            return plane.Raycast(ray, out var entry) ? ray.GetPoint(entry) : Vector3.zero;
        }

        private void HandleMouseZoom()
        {
            var zoomDirection = Vector3.MoveTowards(cameraTransform.localPosition, transform.position, 1).normalized;
            var zoom = _cameraActions.Player.Zoom.ReadValue<float>() / 120;
            if (zoom != 0)
            {
                var newZoom = _newZoom + zoomDirection * zoom * zoomSpeed;
                if (newZoom.y > 10 && newZoom.z < -10) _newZoom = newZoom;
            }
        }

        private void HandleKeyboardMovement()
        {
            var move = _cameraActions.Player.Move.ReadValue<Vector2>();
            _newPosition += transform.forward * move.y * movementSpeed;
            _newPosition += transform.right * move.x * movementSpeed;
        }

        private void HandleKeyboardRotation()
        {
            var rotation = _cameraActions.Player.Rotate.ReadValue<float>();
            _newRotation *= Quaternion.Euler(Vector3.up * rotation * rotationSpeed);
        }
    }

    public interface IFollowable
    {
        public Transform Transform { get; }
    }
}