using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Tests;

public class FundTests
{
    [Fact]
    void It_Should_Calculate_Fund_Value_As_Percentage_Value_Of_Its_Underlying_Investment()
    {
        var fund = new Fund(
            "fundid",
            "investorId",
            "fundInvestorId",
            [
                new Stock("stockid-1", "investorId", new ISIN("isin1"), 2, 2),
                new Stock("stockid-2", "investorId", new ISIN("isin1"), 2, 2),
                new RealEstate("RealEstateId-1", "InvestorId", "Munich", 10, 10),
                new RealEstate("RealEstateId-2", "InvestorId", "Munich", 10, 10),
            ],
            0.5m);
        Assert.Equal(24, fund.Value);
    }

    [Fact]
    void It_Should_Return_Zero_When_Fund_Has_No_Underlying_Investments()
    {
        var fund = new Fund(
            "fundid",
            "investorId",
            "fundInvestorId",
            [],
            0.5m);
        Assert.Equal(0, fund.Value);
    }

    [Fact]
    void It_Should_Return_Zero_When_Investor_Percentage_Is_Zero()
    {
        var fund = new Fund(
            "fundid",
            "investorId",
            "fundInvestorId",
            [
                new Stock("stockid-1", "investorId", new ISIN("isin1"), 2, 2),
                new Stock("stockid-2", "investorId", new ISIN("isin1"), 2, 2),
                new RealEstate("RealEstateId-1", "InvestorId", "Munich", 10, 10),
                new RealEstate("RealEstateId-2", "InvestorId", "Munich", 10, 10),
            ],
            0m);
        Assert.Equal(0, fund.Value);
    }
}