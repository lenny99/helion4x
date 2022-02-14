using System;
using System.Globalization;
using Helion4x.Core;
using Helion4x.Runtime.HelionCamera;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Helion4x.Runtime.Gui
{
    public class SelectableView : MonoBehaviour
    {
        [SerializeField]
        private GameObject population;
        public TextMeshProUGUI Population { get; private set; }

        private SelectableController _controller;
        
        public void Start()
        {
            Population = population.GetComponent<TextMeshProUGUI>();
            _controller = new SelectableController(this);
        }

        private void Update()
        {
            _controller?.Update();
        }
    }

    public class SelectableController
    {
        private readonly SelectableView _view;
        private ISelectable _selectable;
        public SelectableController(SelectableView view)
        {
            _view = view;
            StrategyCamera.Selected += selectable => _selectable = selectable;
            StrategyCamera.Unselected += selectable => selectable = null;
        }

        public void Update()
        {
            if (_selectable != null && _selectable is Settlement settlement)
                _view.Population.text = settlement.Population.ToString();
        }
    }
}