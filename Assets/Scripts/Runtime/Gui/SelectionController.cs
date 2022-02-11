using System;
using Helion4x.Runtime.Camera;
using Helion4x.Runtime.StrategyCamera;
using UnityEngine;

namespace Helion4x.Runtime.Gui
{
    public class SelectionController : MonoBehaviour
    {
        private ISelectable _selectable;
        
        private void Start()
        {
            CloseUpPlayerCamera.Selected += OnSelected;
        }

        private void OnSelected(ISelectable selectable)
        {
            _selectable = selectable;
        }

        private void Update()
        {
            if (_selectable is Fleet fleet)
            {
                // load fleet selection ui and bind the fleet
            }
        }
    }
}