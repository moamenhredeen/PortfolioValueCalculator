using Application.Contracts.Persistence;
using Domain.Abstract;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Persistence.Csv;

public class PortfolioRepository(CsvContext context, ILogger<PortfolioRepository> logger) : IPortfolioRepository
{
    public Portfolio GetPortfolio(string investorId, DateTime date)
    {
        logger.LogDebug($"find investments for investor {investorId}");
        var investments = _getInvestments(investorId, date);
        return new Portfolio(investorId, investments);
    }

    private List<Investment> _getInvestments(string investorId, DateTime date)
    {
         var investments = new List<Investment>(context.Investments[investorId].Count());
        foreach (var investment in context.Investments[investorId])
        {
            switch (investment.InvestmentType)
            {
                case "Fonds":
                    decimal percentage = context.Transactions[investment.InvestmentId]
                        .Where(t =>  t.Date <= date)
                        .Sum(t =>  t.Value);
                    var underlyingInvestments = _getInvestments(investment.FondsInvestor, date);
                    var fundInvestment = new Fund(
                        investment.InvestmentId,
                        investorId,
                        investment.FondsInvestor,
                        underlyingInvestments,
                        percentage);
                    investments.Add(fundInvestment);
                    break;
                case "Stock":
                    var latestQuote = context.Quotes[investment.ISIN]
                        .Where(q => q.Date <= date)
                        .MaxBy(q => (DateTime?)q.Date);
                    var pricePerShare = latestQuote?.PricePerShare ?? 0;
                    decimal numberOfShares = context.Transactions[investment.InvestmentId]
                        .Where(t =>  t.Date <= date)
                        .Sum(t =>  t.Value);
                    var stockInvestment = new Stock(
                        investment.InvestmentId,
                        investorId,
                        new ISIN(investment.ISIN),
                        pricePerShare,
                        numberOfShares);
                    investments.Add(stockInvestment);
                    break;
                case "RealEstate":
                    var building = context.Transactions[investment.InvestmentId]
                        .Where(t => t.Type == "Building" && t.Date <= date)
                        .MaxBy(t => t.Date);
        
                    var estate = context.Transactions[investment.InvestmentId]
                        .Where(t => t.Type == "Estate" && t.Date <= date)
                        .MaxBy(t => t.Date);
                    var realEstateInvestment = new RealEstate(
                        investment.InvestmentId,
                        investorId,
                        investment.City,
                        estate?.Value ?? 0,
                        building?.Value ?? 0);
                    investments.Add(realEstateInvestment);
                    break;
                default:
                    throw new Exception("unknown investment type");
            }
        }
        return investments;
    }
}