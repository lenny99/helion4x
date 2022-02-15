using Godot;
using Helion4x.Runtime;
using Helion4x.Util;

namespace Helion4x.Gui
{
    public class SelectableController : Control
    {
        private Player _player;
        private LineEdit _populationLabel;

        #region Exports

        [Export] private NodePath _populationLabelPath;

        #endregion

        private Selectable _selectable;

        public override void _Ready()
        {
            _player = this.GetPlayer();
            _populationLabel = GetNode<LineEdit>(_populationLabelPath);
            _player.Selected += OnSelected;
        }

        private void OnSelected(Selectable obj)
        {
            _selectable = obj;
        }

        public override void _Draw()
        {
            if (_selectable == null) return;
            if (_selectable.HasSettlement)
                _populationLabel.Text = _selectable.Settlement.Population.ToString();
        }
    }
}