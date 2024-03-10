using LoanCalculator.Calculators;

namespace LoanCalculator.Tests.Calculators
{
    public class IRRCalculatorTests
    {
        [Fact]
        public void CalculateIRR_ReturnsCorrectValue_WithExampleCashFlows()
        {
            var cashFlows = new List<decimal> { -7885.67m, 358.30m, 363.52m, 365.35m, 364.20m, 360.49m, 354.64m, 347.04m, 
                                                338.09m, 328.13m, 317.48m, 306.40m, 201.37m, 283.83m, 272.69m, 261.83m, 251.33m,
                                                241.25m, 137.91m, 222.56m, 213.97m, 205.87m, 198.26m, 191.11m, 184.37m, 178.00m,
                                                171.95m, 166.16m, 160.58m, 155.14m, 149.77m, 144.39m, 138.94m, 133.35m, 127.53m, 
                                                121.39m, 114.71m };

            var result = IRRCalculator.CalculateIRR(cashFlows);
            var expectedIRR = 5.49038m;
            Assert.Equal(expectedIRR, result, precision: 2);
        }
    }
}