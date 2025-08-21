using Domain.ValueObjects;

namespace Application.UseCases.Investor.CalculatePortfolioValue;

public record CalculatePortfolioValueResponse(decimal portfolioValue);