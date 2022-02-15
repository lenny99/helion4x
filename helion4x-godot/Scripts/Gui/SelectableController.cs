using Godot;
using Helion4x.Runtime;

namespace Helion4x.Gui
{
    public class SelectableController : Control
    {
        private LineEdit _populationLabel;
        private Selectable _selectable;

        #region Exports

        [Export] private NodePath populationLabel;

        #endregion

        public override void _Ready()
        {
            _populationLabel = GetNode<LineEdit>(populationLabel);
            EventBus.Selected += s =>
            {
                _selectable = s;
                Show();
            };
            EventBus.Unselected += s =>
            {
                Hide();
                _selectable = null;
            };
        }

        public override void _Draw()
        {
            if (_selectable == null) return;
            if (_selectable.HasSettlement) _populationLabel.Text = _selectable.Settlement.Population.ToString();
        }
    }
}