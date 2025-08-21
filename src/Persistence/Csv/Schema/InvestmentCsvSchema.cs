namespace Persistence.Csv.Schema;

public record InvestmentCsvSchema(string InvestorId, string InvestmentId, string InvestmentType, string? ISIN, string? City, string? FondsInvestor)
{
}