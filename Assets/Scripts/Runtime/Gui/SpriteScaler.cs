using System;
using UnityEngine;

namespace Helion4x.Runtime.Gui
{
    public class SpriteScaler : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private float iconSize;
        private Camera _camera;
        private SpriteRenderer _renderer;
        
        private void Start()
        {
            _camera = Camera.main!;
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            var distance = Vector3.Distance(_camera.transform.position, transform.position);
            _renderer.enabled = distance > 200;
        }
    }
}