using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class UniverseTime : Node
{
    private DateTime time;
    private LastUpdate lastUpdate;

    [Signal]
    public delegate void UpdateUI(String time);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.time = DateTime.Now;
    }

    public void ProgressTime(Dictionary timeToAdd)
    {
        var passedTime = this.AddTime(timeToAdd);

        // foreach (var intervall in intervalls)
        // {
        //     EmitSignal(nameof(TimeProcess), intervall.ToString());
        // }
        EmitSignal(nameof(UpdateUI), time.ToString());
    }

    private TimeSpan AddTime(Dictionary passedTime)
    {
        var pastTime = this.time;
        Func<Intervall, double> getAsDouble = (Intervall intervall) => (double)passedTime[intervall];
        Func<Intervall, int> getAsInt = (Intervall intervall) => (int)passedTime[intervall];
        if (passedTime.Contains(Intervall.Hour))
            this.time = this.time.AddHours(getAsDouble(Intervall.Hour));
        if (passedTime.Contains(Intervall.Day))
            this.time = this.time.AddDays(getAsDouble(Intervall.Day));
        if (passedTime.Contains(Intervall.Month))
            this.time = this.time.AddMonths(getAsInt(Intervall.Month));
        if (passedTime.Contains(Intervall.Year))
            this.time = this.time.AddYears(getAsInt(Intervall.Year));
        return this.time - pastTime;
    }
}



enum Intervall
{
    Year,
    Month,
    Day,
    Hour,
    Minute, // might be needed
    Second // might be needed
}

class LastUpdate : Godot.Object
{
    public DateTime hour { get; set; }
    public DateTime day { get; set; }
    public DateTime month { get; set; }
    public DateTime year { get; set; }

    public LastUpdate()
    {
        this.hour = DateTime.MinValue;
        this.day = DateTime.MinValue;
        this.month = DateTime.MinValue;
        this.year = DateTime.MinValue;
    }

    public LastUpdate(DateTime hour, DateTime day, DateTime month, DateTime year)
    {
        this.hour = hour;
        this.day = day;
        this.month = month;
        this.year = year;
    }

    public List<Intervall> DetermineIntervalls(DateTime time)
    {
        var list = new List<Intervall>(4);
        if (this.hour < time)
        {
            list.Add(Intervall.Hour);
            this.hour = time.AddHours(1);
        }
        if (this.day < time)
        {
            list.Add(Intervall.Day);
            this.day = time.AddDays(1);
        }
        if (this.month < time)
        {
            list.Add(Intervall.Day);
            this.month = time.AddMonths(1);
        }
        if (this.year < time)
        {
            list.Add(Intervall.Year);
            this.year = time.AddYears(1);
        }
        return list;
    }
}