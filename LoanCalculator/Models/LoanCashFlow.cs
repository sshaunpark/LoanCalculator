namespace LoanCalculator.Models
{
    public class LoanCashFlow
    {
        public int Months { get; set; }
        public int PaymentCount { get; set; }
        public DateTime Paydate { get; set; }
        public decimal ScheduledPrincipal { get; set; }
        public decimal ScheduledInterest { get; set; }
        public decimal ScheduledBalance { get; set; }
        public decimal PrepaySpeed { get; set; }
        public decimal DefaultRate { get; set; }
        public decimal Recovery { get; set; }
        public decimal ServicingCF { get; set; }
        public decimal EarnoutCF { get; set; }
        public decimal Balance { get; set; }
        public decimal Principal { get; set; }
        public decimal Default { get; set; }
        public decimal Prepay { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalCF { get; set; }

        public LoanCashFlow(
            int months, 
            int paymentCount, 
            DateTime paydate, 
            decimal scheduledPrincipal, 
            decimal scheduledInterest, 
            decimal scheduledBalance, 
            decimal prepaySpeed, 
            decimal defaultRate, 
            decimal recovery, 
            decimal servicingCF, 
            decimal earnoutCF, 
            decimal balance, 
            decimal principal, 
            decimal @default, 
            decimal prepay, 
            decimal interestAmount, 
            decimal totalCF)
        {
            Months = months;
            PaymentCount = paymentCount;
            Paydate = paydate;
            ScheduledPrincipal = scheduledPrincipal;
            ScheduledInterest = scheduledInterest;
            ScheduledBalance = scheduledBalance;
            PrepaySpeed = prepaySpeed;
            DefaultRate = defaultRate;
            Recovery = recovery;
            ServicingCF = servicingCF;
            EarnoutCF = earnoutCF;
            Balance = balance;
            Principal = principal;
            Default = @default;
            Prepay = prepay;
            InterestAmount = interestAmount;
            TotalCF = totalCF;
        }
    }
}