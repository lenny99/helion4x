using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime
{
    public class AstronomicalCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] [Range(0, 1)] private float movementSpeed;
        [SerializeField] [Range(0, 10)] private float movementTime;
        [SerializeField] [Range(0, 1)] private float rotationSpeed;
        [SerializeField] [Range(0, 10)] private float rotationTime;
        [SerializeField] [Range(0, 5)] private float zoomSpeed;
        [SerializeField] [Range(0, 10)] private float zoomTime;

        private MyPlayerActions _cameraActions;
        private Vector3 _dragCurrent;

        private Vector3 _dragStart;

        private Transform _followTransform;

        private Vector3 _newPosition;
        private Quaternion _newRotation;
        private Vector3 _newZoom;
        private Vector3 _rotateCurrent;
        private Vector3 _rotateStart;

        private void Awake()
        {
            _cameraActions = new MyPlayerActions();
            _cameraActions.Player.Enable();
            _cameraActions.Player.Drag.performed += HandleFirstDrag;
            _cameraActions.Player.DragRotate.performed += HandleFirstMouseRotation;
            _cameraActions.Player.Focus.performed += HandleFocus;
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
            transform.position =
                Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * movementTime);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * rotationTime);
            cameraTransform.localPosition =
                Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * zoomTime);
        }

        private void HandleFocus(InputAction.CallbackContext obj)
        {
            SelectFollow();
            transform.LookAt(_followTransform);
        }

        private void SelectFollow()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var ray = Camera.main!.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var followable = hit.collider.GetComponent<IFollowable>();
            _followTransform = followable.FollowTransform;
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
                var difference = _rotateStart - _rotateCurrent;
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
                var distanceFromRig = Vector3.Distance(transform.position, cameraTransform.position);
                var newZoom = _newZoom + zoomDirection * zoom * Mathf.Sqrt(distanceFromRig * zoomSpeed);
                if (newZoom.y > 0.1 && newZoom.z < -0.1) _newZoom = newZoom;
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
        public Transform FollowTransform { get; }
    }
}