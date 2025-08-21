using System.Globalization;

namespace PortfolioValueCalculator;

public class PortfolioValueCalculator
{
    
    
    ILookup<string, Investment> investmentsMap;
    ILookup<string, Transaction> transactionsMap;
    ILookup<string, Quote> quotesMap;

    public PortfolioValueCalculator(
        TextReader investmentsReader, 
        TextReader transactionsReader, 
        TextReader quoteReader)
    {
        Task.WaitAll(
            Task.Run(() => investmentsMap = _readLines(investmentsReader)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new Investment(cols[0], cols[1], cols[2], cols[3], cols[4], cols[5]))),

            Task.Run(() => transactionsMap = _readLines(transactionsReader)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new Transaction(cols[0], cols[1], DateTime.Parse(cols[2]), cols[3]))),

            Task.Run(() => quotesMap = _readLines(quoteReader)
                .Skip(1)
                .Select(line => line.Split(';'))
                .ToLookup(cols => cols[0], cols => 
                    new Quote(cols[0], DateTime.Parse(cols[1]), cols[2]))));
    }

    public decimal Calculate(string investorId, DateTime date) =>
        investmentsMap[investorId]
            .AsParallel()
            .Sum(investment => investment.InvestmentType switch
        {
            "Stock"      => _calculateStockValue(investment, date),
            "RealEstate" => _calculateRealEstateValue(investment, date),
            "Fonds"      => _calculateFundValue(investment, date),
            _            => 0,
        });

    private decimal _calculateStockValue(Investment investment, DateTime date)
    {
        var latestQuote = quotesMap[investment.ISIN]
            .Where(q => q.Date <= date)
            .MaxBy(q => q.Date);
        var price = _toDecimal(latestQuote?.pricePerShare ?? string.Empty);
        
        decimal numberOfShares = transactionsMap[investment.InvestmentId]
            .Where(t =>  t.Date <= date)
            .Sum(t =>  _toDecimal(t.Value));
        return numberOfShares * price;
    }

    private decimal _calculateRealEstateValue(Investment investment, DateTime date)
    {
        var building = transactionsMap[investment.InvestmentId]
            .Where(t => t.Type == "Building" && t.Date <= date)
            .MaxBy(t => t.Date);
        
        var land = transactionsMap[investment.InvestmentId]
            .Where(t => t.Type == "Estate" && t.Date <= date)
            .MaxBy(t => t.Date);
        
        return _toDecimal(building?.Value) + _toDecimal(land?.Value);
    }
    
    private decimal _calculateFundValue(Investment investment, DateTime date)
    {
        decimal percentage = transactionsMap[investment.InvestmentId]
            .Where(t =>  t.Date <= date)
            .Sum(t =>  _toDecimal(t.Value));
        return percentage * Calculate(investment.FondsInvestor, date);
    }

    private IEnumerable<string> _readLines(TextReader reader)
    {
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    private decimal _toDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0;
        return decimal.Parse(value, NumberStyles.Number, CultureInfo.InvariantCulture);
    } 
}

public sealed record Investment(
    string InvestorId,
    string InvestmentId,
    string InvestmentType,
    string ISIN,
    string City,
    string FondsInvestor) { }

public sealed record  Transaction(
    string InvestmentId, 
    string Type, 
    DateTime Date, 
    string Value) { }

public sealed record Quote(
    string ISIN, 
    DateTime Date, 
    string pricePerShare) { }