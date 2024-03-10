using LoanCalculator.Data;
using LoanCalculator.Models;
using LoanCalculator.Utils;
using Excel.FinancialFunctions;

namespace LoanCalculator.Calculators
{
    public class LoanCashFlowCalculator
    {
        private readonly IDataProvider _dataProvider;

        public LoanCashFlowCalculator(IDataProvider dataProvider)
        {
            // Dependency injection pattern
            _dataProvider = dataProvider;
        }

        public List<LoanCashFlow> GenerateLoanCashFlows(Loan loan, decimal defaultMultiplier = 1.0m, decimal prepayMultiplier = 1.0m)
        {
            var payDates = DateUtil.GenerateDateSeries(loan.IssueDate, IntervalType.Monthly, loan.Term + 1); 
            var loanCashFlows = new List<LoanCashFlow>();
            int paymentCount = 0;
            // Declare the variable as nullable, with C# 8.0 or later
            // lastLoanCashFlow will hold the data of the previous row
            LoanCashFlow? lastLoanCashFlow = null;
            foreach (var payDate in payDates)
            {
                // lastLoanCashFlow == null checks if this is the first row
                var scheduledPrincipal = lastLoanCashFlow == null ? 0.0m :
                    (decimal) Financial.PPmt(
                        (double) (loan.CouponRate / 100.0m / 12.0m),
                        paymentCount,
                        loan.Term,
                        (double) (-1.0m * loan.Invested),
                        0,
                        PaymentDue.EndOfPeriod
                    );

                var scheduledInterest = lastLoanCashFlow == null ? 0.0m :
                    (decimal) Financial.Pmt(
                        (double) (loan.CouponRate / 100.0m / 12.0m),
                        loan.Term,
                        (double) (-1.0m * loan.Invested),
                        0,
                        PaymentDue.EndOfPeriod
                    ) - scheduledPrincipal;

                var scheduledBalance = lastLoanCashFlow == null ? loan.Invested :
                    lastLoanCashFlow.ScheduledBalance - scheduledPrincipal;

                var prepaySpeed = _dataProvider.GetPrepaymentSpeed(loan.Term).ToList();
                var defaultRate = _dataProvider.GetDefaultRate(loan.Term, loan.Grade).ToList();                
                
                var earnoutCF = 0.0m;
                if (paymentCount == 12 || paymentCount == 18) {
                    earnoutCF = loan.Invested * loan.EarnoutFee / 100.0m / 2.0m;
                }

                var @default = lastLoanCashFlow == null ? 0.0m :
                    lastLoanCashFlow.Balance * lastLoanCashFlow.DefaultRate * defaultMultiplier / 100.0m;

                var servicingCF = lastLoanCashFlow == null ? 0.0m :
                    (lastLoanCashFlow.Balance - @default) * loan.ServicingFee / 100.0m / 12.0m;

                var recovery = lastLoanCashFlow == null ? 0.0m :
                    (decimal) loan.RecoveryRate * @default / 100.0m;

                var prepay = lastLoanCashFlow == null ? 0.0m :
                    (lastLoanCashFlow.Balance -
                    (lastLoanCashFlow.Balance - scheduledInterest) *
                    scheduledPrincipal / lastLoanCashFlow.ScheduledBalance) *
                    prepaySpeed[paymentCount] * prepayMultiplier / 100.0m;

                var principal = lastLoanCashFlow == null ? 0.0m :
                    ((lastLoanCashFlow.Balance - @default) * scheduledPrincipal / lastLoanCashFlow.ScheduledBalance) + prepay;

                var balance = lastLoanCashFlow == null ? loan.Invested :
                    lastLoanCashFlow.Balance - principal - @default;

                var interestAmount = lastLoanCashFlow == null ? 0.0m :
                    (lastLoanCashFlow.Balance - @default) * loan.CouponRate / 100.0m / 12.0m;

                var totalCF = lastLoanCashFlow == null ? -1.0m * loan.Invested * (1.0m + (loan.PurchasePremium / 100.0m)) :
                    principal + interestAmount + recovery - servicingCF - earnoutCF;

                lastLoanCashFlow = new LoanCashFlow(
                    months: paymentCount + 1,
                    paymentCount: paymentCount,
                    paydate:payDate,
                    scheduledPrincipal: scheduledPrincipal,
                    scheduledInterest: scheduledInterest,
                    scheduledBalance: scheduledBalance,
                    prepaySpeed: prepaySpeed[paymentCount],
                    defaultRate: defaultRate[paymentCount],
                    recovery: recovery,
                    servicingCF: servicingCF,
                    earnoutCF: earnoutCF,
                    balance: balance,
                    principal: principal,
                    @default: @default,
                    prepay: prepay,
                    interestAmount: interestAmount,
                    totalCF: totalCF
                );
                loanCashFlows.Add(lastLoanCashFlow);
                paymentCount++;
            }
            return loanCashFlows;
        }
    }
}