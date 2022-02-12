using System.Globalization;
using Helion4x.Core;
using Helion4x.Runtime.HelionCamera;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public class SettlementSelector : BaseController
    {
        private TextField _populationField;
        [CanBeNull] private Settlement _settlement;
        private VisualElement _settlementSelection;

        protected override void StartUi()
        {
            StrategyCamera.Selected += OnSelected;
            StrategyCamera.Unselected += OnUnselected;
        }

        protected override void UpdateUi()
        {
            if (_settlement == null) return;
            _populationField.value = _settlement.Population.ToString(CultureInfo.InvariantCulture);
        }

        protected override void BindUi(VisualElement root)
        {
            _settlementSelection = root.Q<VisualElement>("SettlementSelection");
            _populationField = _settlementSelection.Q<TextField>("PopulationField");
        }

        private void OnUnselected(ISelectable obj)
        {
            _settlement = null;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (selectable is Settlement settlement) _settlement = settlement;
        }
    }
}