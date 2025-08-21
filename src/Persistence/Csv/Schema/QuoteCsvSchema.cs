namespace Persistence.Csv.Schema;

public record QuoteCsvSchema(string ISIN, DateTime Date, decimal PricePerShare)
{
}