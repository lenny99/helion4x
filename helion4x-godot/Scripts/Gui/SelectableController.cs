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

        public override void _Ready()
        {
            _player = this.GetPlayer();
            _populationLabel = GetNode<LineEdit>(_populationLabelPath);
        }

        public override void _Process(float delta)
        {
            if (_player?.Selectable != null && _player.Selectable.HasSettlement)
                _populationLabel.Text = _player.Selectable.Settlement.Population.ToString();
        }
    }
}