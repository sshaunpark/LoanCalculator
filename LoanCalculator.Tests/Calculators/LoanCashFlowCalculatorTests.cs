using LoanCalculator.Calculators;
using LoanCalculator.Data;
using LoanCalculator.Models;
using Moq;


namespace LoanCalculator.Tests.Calculators
{
    public class LoanCashFlowCalculatorTests
    {
        private readonly Mock<IDataProvider> mockDataProvider;
        private readonly Loan loan;
        private readonly LoanCashFlowCalculator calculator;

        public LoanCashFlowCalculatorTests()
        {
            mockDataProvider = new Mock<IDataProvider>();
            mockDataProvider.Setup(p => p.GetPrepaymentSpeed(It.IsAny<int>())).Returns(new List<decimal> {   
                0.00000000000000m, 2.47157630951048m, 2.30786643671328m,
                2.23212840797203m, 2.24436222328672m, 2.34456788265734m,
                2.53274538608391m, 2.80889473356643m, 3.17301592510489m,
                3.62510896069930m, 4.16517384034965m, 4.79321056405594m,
                0.00000000000000m});
            mockDataProvider.Setup(p => p.GetDefaultRate(It.IsAny<int>(), It.IsAny<string>())).Returns(new List<decimal> { 
                0.04742795333613m, 0.09896235611298m, 0.13362563659149m,
                0.15561697034244m, 0.16803987712549m, 0.17290222088912m,
                0.17111620977072m, 0.16249839609652m, 0.14576967638161m,
                0.11855529132995m, 0.07738482583438m, 0.00000000000000m,
                0.00000000000000m});

            loan = new Loan(
                grade: "B1",
                issueDate: DateTime.Now,
                term: 12,
                couponRate: 15.0m,
                invested: 10000m,
                outstandingBalance: 5000m,
                recoveryRate: 10.0m,
                purchasePremium: 5.0m,
                servicingFee: 1.0m,
                earnoutFee: 1.0m);

            calculator = new LoanCashFlowCalculator(mockDataProvider.Object);
        }

        [Fact]
        public void GenerateLoanCashFlows_ReturnsExpectedNumberOfCashFlows()
        {
            var result = calculator.GenerateLoanCashFlows(loan);
            Assert.NotNull(result);
            Assert.Equal(loan.Term + 1, result.Count);
        }

        [Fact]
        public void GenerateLoanCashFlows_ValidatesSelectedCashFlows()
        {
            var result = calculator.GenerateLoanCashFlows(loan);
            Assert.NotNull(result);
            // check three random data points
            Assert.Equal(1005.3936m, result[1].Principal, precision: 4);
            Assert.Equal(162.4081m, result[3].Prepay, precision: 4);
            Assert.Equal(755.3688m, result[10].TotalCF, precision: 4);


        }
    }
}
