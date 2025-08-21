using Domain.Abstract;
using Domain.ValueObjects;

namespace Domain .Entities;


/// <summary>
/// Represents a stock investment owned by an investor, identified by an ISIN.
/// </summary>
public class Stock(string id, string investorId, ISIN isin, decimal pricePerShare, decimal numberOfShares) : Investment(id, investorId)
{
    /// <summary>
    /// Gets the International Security Identification Number (ISIN) of the stock.
    /// </summary>
    public ISIN ISIN { get; } = isin;
    
    /// <summary>
    /// Gets the current price per share of the stock.
    /// </summary>
    public decimal PricePerShare { get; } = pricePerShare;
    
    /// <summary>
    /// Gets the number of shares held by the investor.
    /// </summary>
    public decimal NumberOfShares { get; } = numberOfShares;
    
    /// <summary>
    /// Gets the total market value of the stock investment.
    /// </summary>
    public override decimal Value => PricePerShare * NumberOfShares;
}