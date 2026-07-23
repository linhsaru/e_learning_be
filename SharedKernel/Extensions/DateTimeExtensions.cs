namespace SharedKernel.Extensions;

public static class DateTimeExtensions
{
    public static DateTime StartOfDay(this DateTime date) =>
        date.Date;

    public static DateTime EndOfDay(this DateTime date) =>
        date.Date.AddDays(1).AddTicks(-1);

    public static long ToUnixTimestamp(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}
