using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Universe : Node
{
    private DateTime time;
    private float timeWarp;

    private LastUpdate lastUpdate;

    [Signal]
    delegate void TimeProcess(float seconds);

    [Signal]
    delegate void StateChanged(Intervall state);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.time = DateTime.Now;
        this.timeWarp = 1;
        this.lastUpdate = new LastUpdate();
    }

    public String TimeToString()
    {
        return time.ToString();
    }

    public void _on_Timer_timeout()
    {
        this.time = this.time.AddSeconds(timeWarp);
        var intervalls = this.lastUpdate.DetermineIntervalls(time); // TODO
        foreach (var intervall in intervalls)
        {
            EmitSignal(nameof(TimeProcess), intervall);
        }
        EmitSignal(nameof(StateChanged), this);
    }


}

enum Intervall
{
    HOUR,
    DAY,
    MONTH,
    YEAR
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
            list.Add(Intervall.HOUR);
            this.hour = time.AddHours(1);
        }
        if (this.day < time)
        {
            list.Add(Intervall.DAY);
            this.day = time.AddDays(1);
        }
        if (this.month < time)
        {
            list.Add(Intervall.MONTH);
            this.month = time.AddMonths(1);
        }
        if (this.year < time)
        {
            list.Add(Intervall.YEAR);
            this.year = time.AddYears(1);
        }
        return list;
    }
}