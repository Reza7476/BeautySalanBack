using System;
using System.Globalization;

namespace BeautySalon.Utilities;

public static class PersianDateHelper
{
    private static readonly PersianCalendar _persianCalendar = new PersianCalendar();

    /// <summary>
    /// تبدیل یک تاریخ UTC به تاریخ شمسی (DateTime)
    /// ساعت و دقیقه را حفظ می‌کند
    /// </summary>
    public static DateTime ToPersianDateTime(DateTime utcDate)
    {
        // اطمینان از اینکه ورودی UTC است
        var localDate = utcDate.ToLocalTime();

        int year = _persianCalendar.GetYear(localDate);
        int month = _persianCalendar.GetMonth(localDate);
        int day = _persianCalendar.GetDayOfMonth(localDate);
        int hour = localDate.Hour;
        int minute = localDate.Minute;
        int second = localDate.Second;

        return new DateTime(year, month, day, hour, minute, second);
    }

    /// <summary>
    /// تبدیل یک تاریخ UTC به رشته شمسی به فرمت "yyyy/MM/dd"
    /// </summary>
    public static string ToPersianDateString(DateTime utcDate)
    {
        var localDate = utcDate.ToLocalTime();

        int year = _persianCalendar.GetYear(localDate);
        int month = _persianCalendar.GetMonth(localDate);
        int day = _persianCalendar.GetDayOfMonth(localDate);

        return $"{year:D4}/{month:D2}/{day:D2}";
    }

    /// <summary>
    /// تبدیل تاریخ UTC به رشته شمسی با ساعت و دقیقه
    /// فرمت: yyyy/MM/dd HH:mm
    /// </summary>
    public static string ToPersianDateTimeString(DateTime utcDate)
    {
        var localDate = utcDate.ToLocalTime();

        int year = _persianCalendar.GetYear(localDate);
        int month = _persianCalendar.GetMonth(localDate);
        int day = _persianCalendar.GetDayOfMonth(localDate);
        int hour = localDate.Hour;
        int minute = localDate.Minute;

        return $"{year:D4}/{month:D2}/{day:D2} {hour:D2}:{minute:D2}";
    }

    /// <summary>
    /// گرفتن نام روز هفته شمسی
    /// </summary>
    public static string GetPersianDayOfWeek(DateTime utcDate)
    {
        var localDate = utcDate.ToLocalTime();
        DayOfWeek dayOfWeek = localDate.DayOfWeek;

        return dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنج‌شنبه",
            DayOfWeek.Friday => "جمعه",
            _ => ""
        };
    }

    /// <summary>
    /// گرفتن ساعت و دقیقه از DateTime به فرمت "HH:mm"
    /// </summary>
    public static string GetTimeString(DateTime utcDate)
    {
        // تبدیل به زمان محلی (اختیاری)
        var localDate = utcDate.ToLocalTime();

        int hour = localDate.Hour;
        int minute = localDate.Minute;

        return $"{hour:D2}:{minute:D2}";
    }
}
