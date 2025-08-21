using Application.Contracts.Persistence;
using Application.Contracts.UseCase;

namespace Application.UseCases.Investor.CalculatePortfolioValue;

/// <summary>
/// Use-case for calculating the total value of an investor's portfolio on a specific date.
/// </summary>
/// <param name="portfolioRepository"></param>
public class CalculatePortfolioValueUseCase(IPortfolioRepository portfolioRepository) 
    : IUseCase<CalculatePortfolioValueRequest, Result<CalculatePortfolioValueResponse>>
{
    public Result<CalculatePortfolioValueResponse> Execute(CalculatePortfolioValueRequest request)
    {
        try
        {
            var portfolio = portfolioRepository.GetPortfolio(request.investorId, request.date);;
            var value = portfolio.CalculateValue();
            return Result<CalculatePortfolioValueResponse>.Success(new CalculatePortfolioValueResponse(value));
        }
        catch (Exception e)
        {
            return Result<CalculatePortfolioValueResponse>.Failure(e.Message, e);
        }
    }
}