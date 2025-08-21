using Domain.Abstract;

namespace Domain.Entities;

/// <summary>
/// Represents an investment portfolio owned by a single investor.
/// </summary>
/// <remarks>
/// A portfolio is a collection of investments such as stocks, real estate, and funds.
/// The total portfolio value is the sum of the individual investment values.
/// </remarks>
public class Portfolio(string investorId, List<Investment> investments)
{
    /// <summary>
    /// Gets the ID of the investor who owns this portfolio.
    /// </summary>
    public string InvestorId { get; private set; } = investorId;
    
    /// <summary>
    /// Gets the list of investments in the portfolio.
    /// </summary>
    public List<Investment> Investments { get; private set; } = investments;
    
    /// <summary>
    /// Calculates the total value of the portfolio based on the current values of all investments.
    /// </summary>
    /// <returns>The total monetary value of the portfolio.</returns>
    public decimal CalculateValue()
    {
        return Investments.Sum(el => el.Value);
    }
}