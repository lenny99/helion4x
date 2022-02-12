using Helion4x.Core;
using UnityEngine;

namespace Helion4x.Runtime
{
    public class StrategyIcon : MonoBehaviour, IFollowable
    {
        [SerializeField] private float hideAtDistance;

        private Vector3 _baseScale;
        private BoxCollider _collider;
        private Camera _mainCamera;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            _renderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider>();
        }

        private void LateUpdate()
        {
            var distance = Vector3.Distance(transform.localPosition, _mainCamera.transform.localPosition);
            var shouldShowIcon = distance > hideAtDistance;
            _renderer.enabled = shouldShowIcon;
            _collider.enabled = shouldShowIcon;
            if (shouldShowIcon) transform.localScale = _baseScale * Mathf.Sqrt(distance);
        }

        public Transform FollowTransform => transform.parent;
    }
}