using System.Globalization;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace LoanCalculator.Data
{
    public class FileDataProvider : IDataProvider
    {
        private readonly string _defaultRateFilePath;
        private readonly string _prepaymentRateFilePath;

        public FileDataProvider(IConfiguration configuration)
        {
            // Retrieve the file paths from config
            _defaultRateFilePath = configuration.GetValue<string>("FilePaths:DefaultRateFilePath")!
                ?? throw new InvalidOperationException("Default rate file path must be configured in appsettings.json.");
            _prepaymentRateFilePath = configuration.GetValue<string>("FilePaths:PrepaymentRateFilePath")!
                ?? throw new InvalidOperationException("Prepayment speed file path must be configured in appsettings.json.");
        }

        public IEnumerable<decimal> GetDefaultRate(int term, string grade)
        {
            var lines = File.ReadLines(_defaultRateFilePath);
            var headerColumns = lines.First().Split(',');
            var columnIndex = Array.IndexOf(headerColumns, $"{term}-{grade}");

            if (columnIndex == -1)
                throw new KeyNotFoundException($"The term-grade combination {term}-{grade} was not found in the default rates data.");

            return lines
                .Skip(1) // Skip header
                .Select(line => line.Split(','))
                .Where(fields => fields.Length > columnIndex)
                .Select(fields => decimal.TryParse(fields[columnIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rate) ? rate : 0)
                .Take(term + 1);
        }

        public IEnumerable<decimal> GetPrepaymentSpeed(int term)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            var yamlContents = File.ReadAllText(_prepaymentRateFilePath);
            var yamlData = deserializer.Deserialize<Dictionary<string, List<decimal>>>(yamlContents);

            string termKey = $"{term}M";
            if (yamlData.TryGetValue(termKey, out var rates))
            {
                var adjustedRates = new List<decimal> { 0.00000000m };
                adjustedRates.AddRange(rates.Take(term));
                return adjustedRates;
            }

            throw new KeyNotFoundException($"The term {termKey} was not found in the prepayment rate data.");
        }
    }
}