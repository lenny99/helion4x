using Godot;
using Helion4x.Runtime;

namespace Helion4x.Gui
{
    public class SettlementSelectionView : Control
    {
        private LineEdit _gdp;
        private LineEdit _population;
        private LineEdit _tax;

        public Settlement Settlement;

        public override void _Ready()
        {
            _gdp = GetNode<LineEdit>(_gdpPath);
            _population = GetNode<LineEdit>(_populationPath);
            _tax = GetNode<LineEdit>(_taxPath);
        }

        public override void _Process(float delta)
        {
            if (Settlement != null)
            {
                _gdp.Text = Settlement.Gdp.ToString();
                _population.Text = Settlement.Population.ToString();
                _tax.Text = Settlement.Tax.ToString();
            }
        }

        #region Export

        [Export] private NodePath _populationPath;
        [Export] private NodePath _gdpPath;
        [Export] private NodePath _taxPath;

        #endregion
    }
}