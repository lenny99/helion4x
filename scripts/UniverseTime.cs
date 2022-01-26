using Godot;
using Godot.Collections;
using System;

public class UniverseTime : Node
{
    private DateTime time;

    private void setTime(DateTime time)
    {
        this.time = time;
        GetNodeOrNull<Label>("timer/time_menu/time_panel/grid/date_container/text").Text = time.ToString();
    }

    private LastUpdate lastUpdate;

    [Signal]
    public delegate void TimeProgressed(String intervall);

    public override void _Ready()
    {
        setTime(DateTime.Parse("01/01/2030 00:00:00"));
        this.lastUpdate = new LastUpdate(this.time);
    }

    public void _on_Timer_timewarp_timeout()
    {
        ProgressTime(1);
    }

    private void ProgressTime(int hoursPassed)
    {
        for (int hours = 0; hours < hoursPassed; hours++)
        {
            ProgressTimeBy(Intervall.Hour);
            lastUpdate.Hour = lastUpdate.Hour.AddHours(1);
            setTime(time.AddHours(1));
            if (lastUpdate.Day < this.time.AddHours(hours))
            {
                lastUpdate.Hour = lastUpdate.Day.AddDays(1);
                ProgressTimeBy(Intervall.Day);
            }
            if (lastUpdate.Month < this.time.AddHours(hours))
            {
                lastUpdate.Month = lastUpdate.Month.AddHours(hours);
                ProgressTimeBy(Intervall.Month);
            }
            if (lastUpdate.Year < this.time.AddHours(hours))
            {
                lastUpdate.Year = lastUpdate.Year.AddHours(hours);
                ProgressTimeBy(Intervall.Year);
            }
        }
    }

    private void ProgressTimeBy(Intervall intervall)
    {
        EmitSignal(nameof(TimeProgressed), intervall.ToString());
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

struct LastUpdate
{
    public DateTime Hour { get; set; }
    public DateTime Day { get; set; }
    public DateTime Month { get; set; }
    public DateTime Year { get; set; }

    public LastUpdate(DateTime date)
    {
        this.Hour = date;
        this.Day = date;
        this.Month = date;
        this.Year = date;
    }
}