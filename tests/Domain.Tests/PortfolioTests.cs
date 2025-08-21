using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Tests;

public class PortfolioTests
{
    [Fact]
    void It_Should_Calculate_Portfolio_Value_As_Sum_Of_Investments_Values()
    {
        var portfolio = new Portfolio(
            "investorid",
            [
                new Stock("id", "investor-id", new ISIN("ISIN1"), 2, 2),
                new Stock("id", "investor-id", new ISIN("ISIN1"), 2, 2),
                new RealEstate( "RealEstateId", "InvestorId", "Munich", 10, 10),
                new RealEstate( "RealEstateId", "InvestorId", "Munich", 10, 10),
                new Fund( "fundid", "investorId", "fundInvestorId",
                [
                    new Stock("stockid-1", "investorId", new ISIN("isin1"), 2, 2),
                    new Stock("stockid-2", "investorId", new ISIN("isin1"), 2, 2),
                    new RealEstate("RealEstateId-1", "InvestorId", "Munich", 10, 10),
                    new RealEstate("RealEstateId-2", "InvestorId", "Munich", 10, 10),
                ],
                0.5m)
            ]);
        Assert.Equal(72, portfolio.CalculateValue());
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Portfolio_Has_No_Investments()
    {
        var portfolio = new Portfolio(
            "investorid",
            [ ]);
        Assert.Equal(0, portfolio.CalculateValue());
    }
}