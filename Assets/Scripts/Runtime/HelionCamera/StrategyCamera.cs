using System;
using Helion4x.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime.HelionCamera
{
    public class StrategyCamera : MonoBehaviour
    {
        [SerializeField] private Transform marker;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] [Range(0, 1)] private float movementSpeed;
        [SerializeField] [Range(0, 10)] private float movementTime;
        [SerializeField] [Range(0, 1)] private float rotationSpeed;
        [SerializeField] [Range(0, 10)] private float rotationTime;
        [SerializeField] [Range(0, 5)] private float zoomSpeed;
        [SerializeField] [Range(0, 10)] private float zoomTime;

        private Transform _followTransform;
        private Vector3 _markerBaseScale;
        private MyPlayerActions _playerActions;
        private ISelectable _selected;
        private StrategyMouseMovement _strategyMouseMovement;


        public Transform CameraTransform { get; internal set; }
        public Vector3 NewPosition { get; internal set; }
        public Quaternion NewRotation { get; internal set; }
        public Vector3 NewZoom { get; internal set; }
        public Vector3 RotateCurrent { get; internal set; }
        public Vector3 RotateStart { get; internal set; }

        private void Start()
        {
            _markerBaseScale = marker.localScale;
            _playerActions = new MyPlayerActions();
            _playerActions.Enable();
            _playerActions.Player.Click.performed += HandleClick;
            _playerActions.Player.Focus.performed += HandleFocus;
            var my = transform;
            NewPosition = my.position;
            NewRotation = my.rotation;
            CameraTransform = cameraTransform;
            NewZoom = CameraTransform.localPosition;
            _strategyMouseMovement = new StrategyMouseMovement(this, zoomSpeed, rotationSpeed);
        }

        private void Update()
        {
            if (_followTransform != null)
            {
                NewPosition = _followTransform.position;
                if (IsExitingFollow()) _followTransform = null;
            }
            else
            {
                HandleKeyboardMovement();
                _strategyMouseMovement.HandleMouseMovement();
            }

            _strategyMouseMovement.HandleKeyboardRotation();
            _strategyMouseMovement.HandleMouseRotation();
            _strategyMouseMovement.HandleMouseZoom();

            transform.position =
                Vector3.Lerp(transform.position, NewPosition, Time.deltaTime * movementTime);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, NewRotation, Time.deltaTime * rotationTime);
            CameraTransform.localPosition =
                Vector3.Lerp(CameraTransform.localPosition, NewZoom, Time.deltaTime * zoomTime);
        }

        private void LateUpdate()
        {
            cameraTransform.LookAt(transform);
            var distance = CameraUtils.DistanceFromCamera(transform.position);
            marker.localScale = _markerBaseScale * Mathf.Sqrt(distance) / 100;
        }

        public static event Action<ISelectable> Selected = delegate { };
        public static event Action<ISelectable> Unselected = delegate { };

        private bool IsExitingFollow()
        {
            return _playerActions.Player.Focus.WasPerformedThisFrame() ||
                   _playerActions.Player.Move.WasPerformedThisFrame();
        }


        private void HandleClick(InputAction.CallbackContext obj)
        {
            if (_playerActions.Player.Focus.WasPerformedThisFrame()) return;
            var ray = Camera.main!.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit))
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                selectable?.Select();
                _selected = selectable;
            }
            else
            {
                _selected?.Unselect();
                _selected = null;
            }

            Selected.Invoke(_selected);
        }

        private void HandleFocus(InputAction.CallbackContext obj)
        {
            SelectFollow();
            // transform.LookAt(_followTransform);
        }

        private void SelectFollow()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var ray = Camera.main!.ScreenPointToRay(mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var followable = hit.collider.GetComponent<IFollowable>();
            _followTransform = followable.FollowTransform;
        }

        private void HandleKeyboardMovement()
        {
            var move = _playerActions.Player.Move.ReadValue<Vector2>();
            NewPosition += transform.forward * move.y * movementSpeed;
            NewPosition += transform.right * move.x * movementSpeed;
        }
    }
}