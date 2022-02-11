using System;
using Helion4x.Runtime.Camera;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Helion4x.Runtime.StrategyCamera
{
    public class CloseUpPlayerCamera : MonoBehaviour
    {
        public static event Action<ISelectable> Selected = delegate(ISelectable selectable) { };

        [SerializeField] private Transform cameraTransform;
        [SerializeField] [Range(0, 1)] private float movementSpeed;
        [SerializeField] [Range(0, 10)] private float movementTime;
        [SerializeField] [Range(0, 1)] private float rotationSpeed;
        [SerializeField] [Range(0, 10)] private float rotationTime;
        [SerializeField] [Range(0, 5)] private float zoomSpeed;
        [SerializeField] [Range(0, 10)] private float zoomTime;
        
        public Vector3 NewPosition { get; internal set; }
        public Quaternion NewRotation { get; internal set; }
        public Vector3 NewZoom { get; internal set; }
        public Vector3 RotateCurrent { get; internal set; }
        public Vector3 RotateStart { get; internal set; }

        private MyPlayerActions _cameraActions;
        private StrategyMouseMovement _strategyMouseMovement;
        
        private Transform _followTransform;
        private ISelectable _selected;

        private void Awake()
        {
            _cameraActions = new MyPlayerActions();
            _strategyMouseMovement = new StrategyMouseMovement(this);
        }

        private void HandeClick(InputAction.CallbackContext obj)
        {
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit))
            {
                var selectable = hit.collider.GetComponent<ISelectable>();
                if (selectable == null) return;
                _selected = selectable;
            }
            else
            {
                _selected = null;
            }

            Selected.Invoke(_selected);
        }

        private void Start()
        {
            NewPosition = transform.position;
            NewRotation = transform.rotation;
            NewZoom = cameraTransform.localPosition;
            cameraTransform.rotation.SetLookRotation(transform.position);
        }

        private void Update()
        {
            if (_followTransform != null)
            {
                NewPosition = _followTransform.position;
                if (_cameraActions) _followTransform = null;
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
            cameraTransform.localPosition =
                Vector3.Lerp(cameraTransform.localPosition, NewZoom, Time.deltaTime * zoomTime);
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

        public static Vector3 GetPlanePosition()
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var mouseWorldPosition = Camera.main!.ScreenToWorldPoint(mousePosition);
            var ray = Camera.main!.ScreenPointToRay(mouseWorldPosition);
            return plane.Raycast(ray, out var entry) ? ray.GetPoint(entry) : Vector3.zero;
        }

        private void HandleKeyboardMovement()
        {
            var move = StrategyMouseMovement._cameraActions.Player.Move.ReadValue<Vector2>();
            NewPosition += transform.forward * move.y * movementSpeed;
            NewPosition += transform.right * move.x * movementSpeed;
        }
    }
}