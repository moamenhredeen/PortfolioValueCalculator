namespace Application.UseCases.Investor.CalculatePortfolioValue;

public record CalculatePortfolioValueRequest(string investorId, DateTime date);