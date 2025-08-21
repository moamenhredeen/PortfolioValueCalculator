using Domain.Entities;

namespace Application.Contracts.Persistence;

/// <summary>
/// Provides access to investment portfolios.
/// </summary>
public interface IPortfolioRepository
{
    /// <summary>
    /// Retrieves the portfolio for a given investor as of a specified date.
    /// </summary>
    /// <param name="investorId">The unique identifier of the investor.</param>
    /// <param name="date">The date for which the portfolio value should be calculated.</param>
    /// <returns>The portfolio of the investor at the specified date.</returns>
    Portfolio GetPortfolio(string investorId, DateTime date);
}