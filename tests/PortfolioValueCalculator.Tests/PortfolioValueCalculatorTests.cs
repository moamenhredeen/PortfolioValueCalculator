using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace PortfolioValueCalculator.Tests;

public class PortfolioValueCalculatorTests
{
    
    /* --------------------------------------------- Stocks Calculation --------------------------------------------- */
    
    [Fact]
    void It_Should_Return_Zero_When_Portfolio_Is_Empty()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(0, value);
    }
    
    
    [Fact]
    void It_Should_Calculate_Share_Value_As_Price_Times_Count()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2021-01-01;2
            ISIN1;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2017-05-11;1
            Investment1;Shares;2017-05-12;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(4, value);
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Selected_Date_Is_Before_All_Transactions_And_Quotes_Dates()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2021-01-01;2
            ISIN1;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2020-05-11;1
            Investment1;Shares;2020-05-12;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2018, 01, 01));
        Assert.Equal(0, value);
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Selected_Date_Is_Before_Transactions_Date_Only()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2015-01-01;2
            ISIN1;2015-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2020-05-11;1
            Investment1;Shares;2020-05-12;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2018, 01, 01));
        Assert.Equal(0, value);
    }

      
    [Fact]
    void It_Should_Return_Zero_When_Selected_Date_Is_Before_Quotes_Date_Only()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2020-01-01;2
            ISIN1;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2015-05-11;1
            Investment1;Shares;2015-05-12;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2018, 01, 01));
        Assert.Equal(0, value);
    }
    
    
    [Fact]
    void It_Should_Return_Zero_When_Share_Sum_Is_Zero()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2020-01-01;2
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2020-01-01;1
            Investment1;Shares;2020-01-01;-1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(0, value);
    }

    [Fact]
    void It_Should_Return_Negative_Value_When_More_Shares_Are_Sold_Than_Bought()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Stock;ISIN1;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Shares;2020-01-01;1
            Investment1;Shares;2020-01-01;-1
            Investment1;Shares;2020-01-01;-1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(-1, value);
    }
    
    
    /* ----------------------------------------- RealEstate Calculation --------------------------------------------- */
    
    [Fact]
    void It_Should_Return_Zero_When_RealEstate_Has_No_Building_And_No_Estate()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;RealEstate;;City1;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(0, value);
    }
    
    [Fact]
    void It_Should_Return_Zero_When_Selected_Date_Is_Before_Estate_And_Building_Transactions_Dates()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;RealEstate;;City1;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Estate;2021-01-01;1
            Investment1;Building;2020-01-01;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2015, 01, 01));
        Assert.Equal(0, value);
    }
    
    
    [Fact]
    void It_Should_Calculate_RealEstate_Value_As_Sum_Of_Latest_Building_And_Estate_Values()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;RealEstate;;City1;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Estate;2021-01-01;4
            Investment1;Estate;2020-01-01;2
            Investment1;Building;2021-01-01;4
            Investment1;Building;2020-01-01;2
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(8, value);
    }
    
    
    /* ---------------------------------------------- Fund Calculation ---------------------------------------------- */
    
    [Fact]
    void It_Should_Return_Zero_If_Investor_Is_Not_Invested_In_It()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Fonds;;;Fonds1
            Fonds1;Investment2;Stock;ISIN1;;
            Fonds1;Investment3;Stock;ISIN2;;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2020-01-01;1
            ISIN2;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Percentage;2020-01-01;0
            Investment2;Shares;2020-01-01;1
            Investment3;Shares;2020-01-01;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(0, value);
    }
    
      
    [Fact]
    void It_Should_Return_Zero_If_Fund_Has_No_Underlying_Investments()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Fonds;;;Fonds1
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Percentage;2020-01-01;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(0, value);
    }
    
    [Fact]
    void It_Should_Calculate_Fund_Value_As_The_Percentage_Value_Of_Its_Underlying_Investment()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Fonds;;;Fonds1
            Fonds1;Investment2;Stock;ISIN1;;
            Fonds1;Investment3;RealEstate;;City1;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN1;2020-01-01;1
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Percentage;2020-01-01;0.5
            Investment2;Shares;2020-01-01;1
            Investment3;Estate;2021-01-01;1
            Investment3;Building;2020-01-01;1
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);
        
        var value = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(1.5m, value);
    }
    
      
    [Fact]
    void It_Should_Calculate_Fund_Value_As_The_Percentage_Value_Of_Its_Underlying_Investment_Complex()
    {
        var investments = new StringReader(
            """
            InvestorId;InvestmentId;InvestmentType;ISIN;City;FondsInvestor
            Investor1;Investment1;Fonds;;;Fonds1
            Investor1;Investment2;Fonds;;;Fonds2
            Investor2;Investment3;Fonds;;;Fonds1
            Investor2;Investment4;Fonds;;;Fonds2
            Fonds1;Investment5;Stock;ISIN5;;
            Fonds1;Investment6;Stock;ISIN6;;
            Fonds1;Investment7;Stock;ISIN7;;
            Fonds1;Investment8;Stock;ISIN8;;
            Fonds1;Investment9;Stock;ISIN9;;
            Fonds1;Investment10;Stock;ISIN10;;
            Fonds2;Investment11;RealEstate;;City1;
            Fonds2;Investment12;RealEstate;;City1;
            Fonds2;Investment13;RealEstate;;City1;
            Fonds2;Investment14;RealEstate;;City1;
            Fonds2;Investment15;RealEstate;;City1;
            """);

        var quotes = new StringReader(
            """
            ISIN;Date;PricePerShare
            ISIN5;2020-01-01;5
            ISIN6;2020-01-01;6
            ISIN7;2020-01-01;7
            ISIN8;2020-01-01;8
            ISIN9;2020-01-01;9
            ISIN10;2020-01-01;10
            """);

        var transactions = new StringReader(
            """
            InvestmentId;Type;Date;Value
            Investment1;Percentage;2020-01-01;0.5
            Investment2;Percentage;2020-01-01;0.5
            Investment3;Percentage;2020-01-01;0.5
            Investment4;Percentage;2020-01-01;0.5
            Investment5;Shares;2021-01-01;5
            Investment6;Shares;2021-01-01;6
            Investment7;Shares;2021-01-01;7
            Investment8;Shares;2021-01-01;8
            Investment9;Shares;2021-01-01;9
            Investment10;Shares;2021-01-01;10
            Investment11;Estate;2021-01-01;11
            Investment11;Building;2020-01-01;11
            Investment12;Estate;2021-01-01;12
            Investment12;Building;2020-01-01;12
            Investment13;Estate;2021-01-01;13
            Investment13;Building;2020-01-01;13
            Investment14;Estate;2021-01-01;14
            Investment14;Building;2020-01-01;14
            Investment15;Estate;2021-01-01;15
            Investment15;Building;2020-01-01;15
            """);

        var calculator = new PortfolioValueCalculator(
            investmentsReader: investments,
            transactionsReader: transactions,
            quoteReader: quotes);

        decimal expectedValue = (
                (5 * 5) + (6 * 6) + (7 * 7) + (8 * 8) + (9 * 9) + (10 * 10) + // Shares 
                11 + 11 + 12 + 12 + 13 + 13 + 14 + 14 + 15 + 15 // RealEstates
            ) * 0.5m; // Fund Percentage
        var investor1PortfolioValue = calculator.Calculate("Investor1", new DateTime(2022, 01, 01));
        Assert.Equal(expectedValue, investor1PortfolioValue);
        
        var investor2PortfolioValue = calculator.Calculate("Investor2", new DateTime(2022, 01, 01));
        Assert.Equal(expectedValue, investor1PortfolioValue);
    }

}