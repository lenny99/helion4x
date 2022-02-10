using UnityEngine;

namespace Helion4x.Runtime
{
    public class IconManager : MonoBehaviour
    {
        [SerializeField] private GameObject icon;
        private Vector3 _baseScale;
        private Transform _cameraTransform;
        private MeshRenderer _renderer;

        private void Start()
        {
            _baseScale = transform.localScale;
            _cameraTransform = Camera.main!.transform;
            _renderer = GetComponent<MeshRenderer>();
        }

        private void LateUpdate()
        {
            var distance = Vector3.Distance(transform.position, _cameraTransform.position);
            var shouldShowIcon = distance > 200;
            _renderer.enabled = !shouldShowIcon;
            icon.SetActive(shouldShowIcon);
            if (shouldShowIcon) transform.localScale = _baseScale * 10;
        }
    }
}