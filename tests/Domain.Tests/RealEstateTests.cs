using Domain.Entities;

namespace Domain.Tests;

public class RealEstateTests
{
    [Fact]
    void It_Should_Calculate_RealEstate_Value_As_Sum_Of_Latest_Building_And_Estate_Values()
    {
        var realEstate = new RealEstate(
            "RealEstateId",
            "InvestorId",
            "Munich",
            10,
            10);
        
        Assert.Equal(20, realEstate.Value);
    }
    
    [Fact]
    void It_Should_Return_Estate_Value_When_There_Are_No_Buildings()
    {
        var realEstate = new RealEstate(
            "RealEstateId",
            "InvestorId",
            "Munich",
            10,
            0);
        Assert.Equal(10, realEstate.Value);
    }
    
    [Fact]
    void It_Should_Return_Latest_Building_Value_When_There_Are_No_Estate_Values()
    {
        var realEstate = new RealEstate(
            "RealEstateId",
            "InvestorId",
            "Munich",
            0,
            10);
        Assert.Equal(10, realEstate.Value);
    }
}