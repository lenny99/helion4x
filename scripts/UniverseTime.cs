using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class UniverseTime : Node
{
    private DateTime time
    {
        get
        {
            return this.time;
        }
        set
        {
            EmitSignal(nameof(UpdateUI), value);
            this.time = value;
        }
    }
    private LastUpdate lastUpdate;

    [Signal]
    public delegate void UpdateUI(String time);
    [Signal]
    public delegate void TimeProgressed(Intervall intervall);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.time = new DateTime(2030, 1, 1);
        this.lastUpdate = new LastUpdate(this.time);
        EmitSignal(nameof(UpdateUI), this.time.ToString());
    }

    public void ProgressTime(Dictionary timePassed)
    {
        var hoursPassed = calculateHoursPassed(timePassed);
        for (int hours = 1; hours < hoursPassed; hours++)
        {
            ProgressTimeBy(Intervall.Hour);
            lastUpdate = lastUpdate.Hour.Add(1);
            time = time.AddHours(1);
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
        EmitSignal(nameof(TimeProgressed), intervall);
    }

    private int calculateHoursPassed(Dictionary<int> passedTime)
    {
        return passedTime[Intervall.Hour] + passedTime[Intervall.Day] * 24;
    }
}

enum Intervall
{
    Year,
    Month,
    Day,
    Hour,
}

struct LastUpdate
{
    public DateTime Hour { get; set; }
    public DateTime Day { get; set; }
    public DateTime Month { get; set; }
    public DateTime Year { get; set; }

    public UniverseTime(DateTime date)
    {
        this.Hour = date;
        this.Day = date;
        this.Month = date;
        this.Year = date;
    }
}