using Excel.FinancialFunctions;

namespace LoanCalculator.Calculators
{
    public static class IRRCalculator
    {
        public static decimal CalculateIRR(List<decimal> cashFlows)
        {
            var cashFlowsDouble = cashFlows.Select(cf => (double) cf).ToList();
            var irr = Financial.Irr(cashFlowsDouble);
            return (decimal) irr * 100.0m * 12.0m;
        }
    }
}