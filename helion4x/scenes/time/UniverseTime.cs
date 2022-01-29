using Godot;
using Godot.Collections;
using System;

public class UniverseTime : Node
{
    [Signal]
    public delegate void state_changed(Dictionary state);

    private DateTime time;

    private void setTime(DateTime time)
    {
        this.time = time;
        var state = new Dictionary();
        state["time"] = this.time.ToString();
        EmitSignal(nameof(state_changed), state);
    }

    private NextUpdate nextUpdate;

    public override void _Ready()
    {
        setTime(DateTime.Parse("01/01/2030 00:00:00"));
        this.nextUpdate = new NextUpdate(this.time);
    }

    public void _on_timer_timewarp_timeout()
    {
        ProgressTime();
    }

    private void ProgressTime()
    {
        if (nextUpdate.Hour <= this.time)
        {
            nextUpdate.Hour = time.AddHours(1);
            ProgressTimeBy(Intervall.Hour);
        }
        if (nextUpdate.Day <= this.time)
        {
            nextUpdate.Day = time.AddDays(1);
            ProgressTimeBy(Intervall.Day);
        }
        if (nextUpdate.Month <= this.time)
        {
            nextUpdate.Month = time.AddMonths(1);
            ProgressTimeBy(Intervall.Month);
        }
        if (nextUpdate.Year <= this.time)
        {
            nextUpdate.Year = time.AddYears(1);
            ProgressTimeBy(Intervall.Year);
        }
        setTime(time.AddHours(1));
    }

    private void ProgressTimeBy(Intervall intervall)
    {
        GetTree().CallGroup("Timeables", "_time_process", intervall.ToString());
    }
}

enum Intervall
{
    Year,
    Month,
    Day,
    Hour,
}

enum Timewarp
{
    Slow,
    Normal,
    Faster,
    Fastest
}

static class TimewarpUtils
{
    public static Timewarp? StringToTimewarp(String timewarp)
    {
        switch (timewarp)
        {
            case "0": return Timewarp.Slow;
            case "1": return Timewarp.Normal;
            case "2": return Timewarp.Faster;
            case "3": return Timewarp.Fastest;
            default: return null;
        }
    }
}

struct NextUpdate
{
    public DateTime Hour { get; set; }
    public DateTime Day { get; set; }
    public DateTime Month { get; set; }
    public DateTime Year { get; set; }

    public NextUpdate(DateTime date)
    {
        this.Hour = date;
        this.Day = date;
        this.Month = date;
        this.Year = date;
    }
}