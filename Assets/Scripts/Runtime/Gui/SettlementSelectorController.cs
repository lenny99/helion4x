using System.Globalization;
using Helion4x.Core;
using Helion4x.Runtime.HelionCamera;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public class SettlementSelectorController : MonoBehaviour
    {
        private readonly bool _shouldHide = true;
        private VisualElement _container;
        private Settlement _settlement;
        private SettlementSelectorView _view;

        private void Start()
        {
            var root = GetComponentInParent<UIDocument>().rootVisualElement;
            _container = root.Q<VisualElement>("SelectorContainer");
            StrategyCamera.Selected += OnSelected;
            StrategyCamera.Unselected += OnUnselected;
        }

        private void Update()
        {
            if (_settlement == null) return;
            _view.Population.value = _settlement.Population.ToString(CultureInfo.InvariantCulture);
        }

        private void CreateAndAdd()
        {
            _view?.Element.RemoveFromHierarchy();
            _view = new SettlementSelectorView();
            _container.Add(_view.Element);
        }

        private void OnUnselected(ISelectable selectable)
        {
            _settlement = null;
            if (_shouldHide) _view.RemoveFromHierarchy();
        }

        private void OnSelected(ISelectable selectable)
        {
            if (!(selectable is Settlement settlement)) return;
            _settlement = settlement;
            CreateAndAdd();
        }
    }
}