﻿namespace CashService.BusinessLogic.Extensions
{
    public static class DateTimeOffsetExtension
    {
        public static bool Between(this DateTimeOffset date, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return startDate.Date <= date.Date && date <= endDate.Date;
        }
    }
}
