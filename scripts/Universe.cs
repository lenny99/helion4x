using Godot;
using System;

public class Universe : Node
{
    private DateTime time;
    private float timeWarp;

    [Signal]
    delegate void TimeProcess(float seconds);

    [Signal]
    delegate void StateChanged(Universe state);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.time = DateTime.Now;
        this.timeWarp = 1;
    }

    public void _on_Timer_timeout()
    {
        this.time = this.time.AddSeconds(timeWarp);
        EmitSignal(nameof(TimeProcess), timeWarp);
        EmitSignal(nameof(StateChanged), this);
    }

    public String TimeToString()
    {
        return time.ToString();
    }
}