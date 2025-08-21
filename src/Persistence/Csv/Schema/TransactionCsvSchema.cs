namespace Persistence.Csv.Schema;

public record TransactionCsvSchema(string InvestmentId, string Type, DateTime Date, decimal Value)
{
}