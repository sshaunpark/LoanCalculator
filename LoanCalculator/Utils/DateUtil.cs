namespace LoanCalculator.Utils
{
    public static class DateUtil
    {
        // Generates a series of dates starting from startDate, repeating at intervalType for periodCount times
        public static List<DateTime> GenerateDateSeries(DateTime startDate, IntervalType intervalType, int periodCount)
        {
            var dateSeries = new List<DateTime>();

            for (int i = 0; i < periodCount; i++)
            {
                switch (intervalType)
                {
                    case IntervalType.Daily:
                        dateSeries.Add(startDate.AddDays(i));
                        break;
                    case IntervalType.Weekly:
                        dateSeries.Add(startDate.AddDays(i * 7));
                        break;
                    case IntervalType.Monthly:
                        dateSeries.Add(startDate.AddMonths(i));
                        break;
                    case IntervalType.Yearly:
                        dateSeries.Add(startDate.AddYears(i));
                        break;
                }
            }

            return dateSeries;
        }
    }

    public enum IntervalType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}