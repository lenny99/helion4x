using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public class SettlementSelectorView
    {
        private bool _isMouseOverWindow;

        public SettlementSelectorView()
        {
            var uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Views/SettlementSelection.uxml");
            var tree = uiAsset.CloneTree();
            Element = tree;
            Population = tree.Q<TextField>("PopulationField");
        }

        public TextField Population { get; }
        public VisualElement Element { get; }

        public void RemoveFromHierarchy()
        {
            var position = Mouse.current.position.ReadValue();
            var isMouseOverElement = Element.ContainsPoint(position);
            if (!isMouseOverElement) Element.RemoveFromHierarchy();
        }
    }
}