using LoanCalculator.Models;

namespace LoanCalculator.Tests.Models
{
    public class LoanTests
    {
        [Fact]
        public void Loan_WithValidParameters_ShouldCreateSuccessfully()
        {
            var validLoan = new Loan(
                grade: "A",
                issueDate: DateTime.Now,
                term: 12,
                couponRate: 5.0m,
                invested: 10000m,
                outstandingBalance: 5000m,
                recoveryRate: 50.0m,
                purchasePremium: 2.0m,
                servicingFee: 1.0m,
                earnoutFee: 1.0m);
            Assert.NotNull(validLoan);
        }

    }
}