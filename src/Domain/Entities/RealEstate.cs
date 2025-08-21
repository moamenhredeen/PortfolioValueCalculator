using Domain.Abstract;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a real estate investment consisting of land and a building located in a specific city.
/// </summary>
public class RealEstate(string id, string investorId, string city, decimal estateValue, decimal buildingValue) : Investment(id, investorId)
{
    /// <summary>
    /// Gets the city where the real estate is located.
    /// </summary>
    public string City { get; } = city;
    
    /// <summary>
    /// Gets the value of the land (estate) of the property.
    /// </summary>
    public decimal EstateValue {get;} = estateValue;
    
    
    /// <summary>
    /// Gets the value of the building component of the property.
    /// </summary>
    public decimal BuildingValue {get;} = buildingValue; 

    /// <summary>
    /// Gets the total value of the real estate investment.
    /// </summary>
    public override decimal Value => EstateValue + BuildingValue;
}