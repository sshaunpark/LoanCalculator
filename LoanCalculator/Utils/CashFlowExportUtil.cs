using System.Globalization;
using LoanCalculator.Models;

namespace LoanCalculator.Utils
{
    public static class CashFlowExportUtil
    {
        public static void ExportToCsv(IEnumerable<LoanCashFlow> cashFlows, string filePath)
        {
            var lines = new List<string>();
            var headerParts = new List<string>
            {
                "Month",
                "PaymentCount",
                "PayDate",
                "ScheduledPrincipal",
                "ScheduledInterest",
                "ScheduledBalance",
                "PrepaySpeed",
                "DefaultRate",
                "Recovery",
                "ServicingCF",
                "EarnoutCF",
                "Balance",
                "Principal",
                "Default",
                "Prepay",
                "InterestAmount",
                "TotalCF"
            };
            
            var headerLine = string.Join(",", headerParts);
            lines.Add(headerLine);

            lines.AddRange(cashFlows.Select(cf => 
                $"{cf.Months}," +
                $"{cf.PaymentCount}," +
                $"{cf.Paydate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}," +
                $"{Math.Round(cf.ScheduledPrincipal, 2)}," +
                $"{Math.Round(cf.ScheduledInterest, 2)}," +
                $"{Math.Round(cf.ScheduledBalance, 2)}," +
                $"{Math.Round(cf.PrepaySpeed, 4)}%," +
                $"{Math.Round(cf.DefaultRate, 4)}%," +
                $"{Math.Round(cf.Recovery, 2)}," +
                $"{Math.Round(cf.ServicingCF, 2)}," +
                $"{Math.Round(cf.EarnoutCF, 2)}," +
                $"{Math.Round(cf.Balance, 2)}," +
                $"{Math.Round(cf.Principal, 2)}," +
                $"{Math.Round(cf.Default, 2)}," +
                $"{Math.Round(cf.Prepay, 2)}," +
                $"{Math.Round(cf.InterestAmount, 2)}," +
                $"{Math.Round(cf.TotalCF, 2)}"));

            File.WriteAllLines(filePath, lines);
        }
    }
}
