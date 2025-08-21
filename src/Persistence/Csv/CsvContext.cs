using System.Globalization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Csv.Schema;

namespace Persistence.Csv;

public class CsvContext(IConfiguration configuration, ILogger<CsvContext> logger)
{
    public ILookup<string, InvestmentCsvSchema> Investments { get; private set;  }
    public ILookup<string, TransactionCsvSchema> Transactions { get; private set; }
    public ILookup<string, QuoteCsvSchema> Quotes { get; private set; }

    public void Load()
    {
        logger.LogDebug("Loading CSV files");
        var investments = Path.Combine(configuration["DataFolder"]!, "investments.csv");
        var transactions = Path.Combine(configuration["DataFolder"]!, "transactions.csv");
        var quotes = Path.Combine(configuration["DataFolder"]!, "quotes.csv");
        
        Task.WaitAll(
            Task.Run(() => Investments = File.ReadAllLines(investments)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new InvestmentCsvSchema(cols[0], cols[1], cols[2], cols[3], cols[4], cols[5]))),

            Task.Run(() => Transactions = File.ReadLines(transactions)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new TransactionCsvSchema(cols[0], cols[1], DateTime.Parse(cols[2]), _toDecimal(cols[3])))),

            Task.Run(() => Quotes = File.ReadLines(quotes)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new QuoteCsvSchema(cols[0], DateTime.Parse(cols[1]), _toDecimal(cols[2])))));
        logger.LogDebug("Data Loaded Successfully");
    }
    
    private decimal _toDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0;
        return decimal.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
    } 
}