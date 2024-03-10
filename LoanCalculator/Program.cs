using System.Globalization;
using Microsoft.Extensions.Configuration;
using LoanCalculator.Data;
using LoanCalculator.Utils;
using LoanCalculator.Models;
using LoanCalculator.Calculators;


class Program
{
    static void Main(string[] args)
    {            
        // Config to read appsettings.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();

        // Initialize FileDataProvider with configuration
        var dataProvider = new FileDataProvider(configuration);

        // Same set of terms as the example on Loan IRR Spreadsheet
        var loan = new Loan(
            grade: "C4",
            issueDate: DateTime.ParseExact("08/24/15", "MM/dd/yy", CultureInfo.InvariantCulture),
            term: 36,
            couponRate: 28.0007632124385m,
            invested: 7500.0m,
            outstandingBalance: 3228.61m,
            recoveryRate: 8.0m,
            purchasePremium: 5.1422082m,
            servicingFee: 2.5m,
            earnoutFee: 2.5m
        );

        // Initialize LoanCashFlowCalculator with dependency injection for flexible data providers, 
        // enabling easy testing with mock providers.
        var loanCashFlowCalculator = new LoanCashFlowCalculator(dataProvider);
        var loanCashFlows = loanCashFlowCalculator.GenerateLoanCashFlows(loan);
        // The csv output contains same data as the Loan IRR spreadsheet
        CashFlowExportUtil.ExportToCsv(loanCashFlows, "example_output.csv");
        // IRR is only printed to the console
        var irr = IRRCalculator.CalculateIRR(loanCashFlows.Select(lcf => lcf.TotalCF).ToList());
        Console.WriteLine($"IRR is {Math.Round(irr,4)}%");
    }
}