using LoanCalculator.Utils;

namespace LoanCalculator.Tests.Utils
{
    public class DateUtilTests
    {
        [Theory]
        [InlineData(IntervalType.Daily, 3)]
        [InlineData(IntervalType.Weekly, 2)]
        [InlineData(IntervalType.Monthly, 4)]
        [InlineData(IntervalType.Yearly, 1)]
        public void GenerateDateSeries_ShouldGenerateCorrectNumberOfSeries(IntervalType intervalType, int periodCount)
        {
            var startDate = new DateTime(2023, 1, 1);

            var dates = DateUtil.GenerateDateSeries(startDate, intervalType, periodCount);

            Assert.NotNull(dates);
            Assert.Equal(periodCount, dates.Count);

            for (int i = 0; i < periodCount; i++)
            {
                DateTime expectedDate = intervalType switch
                {
                    IntervalType.Daily => startDate.AddDays(i),
                    IntervalType.Weekly => startDate.AddDays(7 * i),
                    IntervalType.Monthly => startDate.AddMonths(i),
                    IntervalType.Yearly => startDate.AddYears(i),
                    _ => throw new ArgumentOutOfRangeException(nameof(intervalType), $"Unexpected interval type: {intervalType}")
                };

                Assert.Equal(expectedDate, dates[i]);
            }
        }
    }
}
