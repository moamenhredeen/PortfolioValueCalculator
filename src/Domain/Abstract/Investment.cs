namespace Domain.Abstract;

/// <summary>
/// Represents a generic investment belonging to an investor.
/// Base class for specific investment types like stocks, real estate, or funds.
/// </summary>
public abstract class Investment(string id, string investorId)
{
    /// <summary>
    /// Gets the unique identifier of the investment.
    /// </summary>
    public string Id { get; private set; } = id;
    
    /// <summary>
    /// Gets the identifier of the investor who owns this investment.
    /// </summary>
    public string InvestorId { get; private set; } = investorId;
    
    /// <summary>
    /// Gets the current value of the investment.
    /// Must be implemented by derived types.
    /// </summary>
    public abstract decimal Value { get; }
}