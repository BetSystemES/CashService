namespace CashService.BusinessLogic.Extensions
{
    public static class DateTimeOffsetExtension
    {
        public static bool Between(this DateTimeOffset date, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return startDate.CompareTo(date) == -1 && date.CompareTo(endDate) == -1;
        }
    }
}
