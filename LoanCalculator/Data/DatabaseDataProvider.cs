namespace LoanCalculator.Data
{
    public class DatabaseDataProvider : IDataProvider
    {
        public IEnumerable<decimal> GetDefaultRate(int term, string grade)
        {
            // Placeholder for pulling DefaultRate from a Database
            return [];
        }

        public IEnumerable<decimal> GetPrepaymentSpeed(int term)
        {
            // Placeholder for pulling PrepaymentSpeed from a Database
            return [];
        }
    }
}