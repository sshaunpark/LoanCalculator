namespace LoanCalculator.Data
{
    public interface IDataProvider
    {
        IEnumerable<decimal> GetDefaultRate(int term, string grade);
        IEnumerable<decimal> GetPrepaymentSpeed(int term);
    }
}