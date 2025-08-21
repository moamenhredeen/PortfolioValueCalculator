using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Tests;

public class StockTests
{

    [Fact]
    void It_Should_Calculate_Stock_Value_As_Share_Price_Times_Count()
    {
        var stock = new Stock("id", "investor-id", new ISIN("ISIN1"), 2, 2);
        Assert.Equal(4, stock.Value);
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Share_Count_Is_Zero()
    {
        var stock = new Stock("id", "investor-id", new ISIN("ISIN1"), 2, 0);
        Assert.Equal(0, stock.Value);
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Share_Price_Is_Zero()
    {
        var stock = new Stock("id", "investor-id", new ISIN("ISIN1"), 2, 0);
        Assert.Equal(0, stock.Value);
    }
}