using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public class Universe : Node
{
    private DateTime time;
    private float timeWarp;

    private LastUpdate lastUpdate;

    [Signal]
    delegate void TimeProcess(Intervall intervall);

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
            EmitSignal(nameof(TimeProcess), intervall.ToString());
        }
        EmitSignal(nameof(StateChanged), this);
    }

    public void _on_Menu_progress_intervall(int i)
    {
        Intervall intervall = fromInt(i);

    }

    private TimeSpan timeSpanForIntervall(Intervall intervall)
    {
        switch (intervall)
        {
            case Intervall.HOUR:
                return TimeSpan.FromHours(1);
            case Intervall.DAY:
                return TimeSpan.FromDays(1);
            case Intervall.MONTH:
                int daysInMonth = DateTime.DaysInMonth(this.time.Year, this.time.Month);
                return TimeSpan.FromDays(daysInMonth);
            case Year
        }
    }

    private Intervall fromInt(int i)
    {
        switch (i)
        {
            case 0: return Intervall.NONE;
            case 1: return Intervall.HOUR;
            case 2: return Intervall.DAY;
            case 4: return Intervall.MONTH;
            case 5: return Intervall.YEAR;
            default: return Intervall.NONE;
        }
    }
}

enum Intervall
{
    NONE,
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