using System.Globalization;
using Godot;
using Helion4x.Runtime;

namespace Helion4x.Gui
{
    public class TimeController : Control
    {
        private Button _fasterButton;
        private Button _pauseButton;
        private Button _slowerButton;
        private Label _timeLabel;

        private TimeManager _timeManager;


        public override void _Ready()
        {
            _timeManager = GetNode<TimeManager>(timeManagerPath);
            _timeLabel = GetNode<Label>(timeLabelPath);
            _slowerButton = GetNode<Button>(slowerButtonPath);
            _pauseButton = GetNode<Button>(pauseButtonPath);
            _fasterButton = GetNode<Button>(fasterButtonPath);
        }

        public override void _Process(float delta)
        {
            _timeLabel.Text = _timeManager.Time.ToString(CultureInfo.InvariantCulture);
        }

        #region Exports

        [Export] private NodePath timeManagerPath;
        [Export] private NodePath timeLabelPath;
        [Export] private NodePath slowerButtonPath;
        [Export] private NodePath pauseButtonPath;
        [Export] private NodePath fasterButtonPath;

        #endregion
    }
}