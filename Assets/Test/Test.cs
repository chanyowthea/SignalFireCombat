using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        var date = GetRaceStartDate(201843, 2);
        Debug.Log("year=" + date);
    }

    System.DateTime GetRaceStartDate(uint latestWeekID, uint startWeekDay)
    {
        uint weeks = 0;
        weeks = latestWeekID % 100;
        uint year = 0;
        year = latestWeekID / 100;
        System.DateTime time = new System.DateTime((int)year, 1, 1);
        time = time.AddDays((weeks - 1) * 7);
        if (time.DayOfWeek != System.DayOfWeek.Monday)
        {
            time = time.AddDays(-DayOfWeekToInteger(time.DayOfWeek) - 1);
        }
        if (startWeekDay != 1)
        {
            time = time.AddDays(startWeekDay - 1);
        }
        return time; 
    }

    public uint DayOfWeekToInteger(System.DayOfWeek weekday)
    {
        return weekday == System.DayOfWeek.Sunday ? 7 : (uint)weekday;
    }

    public System.DayOfWeek IntegerToDayOfWeek(uint weekday)
    {
        return weekday == 7 ? System.DayOfWeek.Sunday : (System.DayOfWeek)weekday;
    }
}
