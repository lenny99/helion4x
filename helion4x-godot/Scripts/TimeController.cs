using Godot;
using System;
using System.Globalization;
using Helion4x.Core.Time;
using Helion4x.Scripts;

public class TimeController : Control, ITimeable
{
    [Export] private NodePath _universeTimePath;
    private UniverseTime _universeTime;
    [Export] private NodePath _timewarpPath;
    private TimewarpTimer _timewarp;
    
    private Label _timeLabel;
    private Button _fasterButton;
    private Button _pauseButton;
    private Button _slowerButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup(nameof(ITimeable));
        _universeTime = GetNode<UniverseTime>(_universeTimePath);
        _timewarp = GetNode<TimewarpTimer>(_timewarpPath);
        _timeLabel = GetNode<Label>("VBoxContainer/PanelContainer/TimeLabel");
        _fasterButton = GetNode<Button>("VBoxContainer/HBoxContainer/Faster");
        _pauseButton = GetNode<Button>("VBoxContainer/HBoxContainer/Pause");
        _slowerButton = GetNode<Button>("VBoxContainer/HBoxContainer/Slower");
        _fasterButton.Connect("pressed", this, nameof(OnFasterButtonPressed));
        _pauseButton.Connect("pressed", this, nameof(OnPauseButtonPressed));
        _slowerButton.Connect("pressed", this, nameof(OnSlowerButtonPressed));
    }

    public void TimeProcess(Interval interval)
    {
        _timeLabel.Text = _universeTime.Time.ToString(CultureInfo.InvariantCulture);
    }

    public void OnSlowerButtonPressed()
    {
        _timewarp.SlowDown();
        _pauseButton.Text = _timewarp.Timewarp.ToString();
    }

    public void OnPauseButtonPressed()
    {
        _timewarp.Pause();
        _pauseButton.Text = _timewarp.Timewarp.ToString();
    }

    public void OnFasterButtonPressed()
    {
        _timewarp.SpeedUp();
        _pauseButton.Text = _timewarp.Timewarp.ToString();
    }
}
