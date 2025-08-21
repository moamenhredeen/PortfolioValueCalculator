using Domain.Abstract;

namespace Domain.Entities;

/// <summary>
/// Represents a fund — a financial entity that pools capital to invest in a collection of underlying assets.
/// </summary>
/// <remarks>
/// A fund can hold various types of investments such as stocks or real estate, or even other funds.
/// It may be owned by an individual investor or by another fund (in the case of nested or hierarchical fund structures).
/// Ownership in a fund is typically represented as a percentage, indicating the investor's share of the total fund.
/// </remarks>
public class Fund(string id, string investorId, string fundInvestor, List<Investment> underlyingInvestments, decimal percentage) : Investment(id, investorId)
{
    /// <summary>
    /// Gets the identifier of the investor entity that owns this fund (e.g., a parent fund or investor).
    /// </summary>
    public string FundInvestor { get; } = fundInvestor;
    
    /// <summary>
    /// Gets the percentage ownership in the underlying investments.
    /// </summary>
    public decimal Percentage { get; } = percentage;
    
    /// <summary>
    /// Gets the list of investments held by this fund.
    /// </summary>
    public List<Investment> UnderlyingInvestments { get; } = underlyingInvestments;
    
    /// <summary>
    /// Gets the calculated value of the fund as a weighted sum of its underlying investments.
    /// </summary>
    public override decimal Value => Percentage * UnderlyingInvestments.Sum(el => el.Value);
}