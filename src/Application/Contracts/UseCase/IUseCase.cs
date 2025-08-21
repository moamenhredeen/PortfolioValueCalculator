namespace Application.Contracts.UseCase;

/// <summary>
/// Defines a use case with a request and a response.
/// </summary>
/// <typeparam name="TRequest">The type of the input request.</typeparam>
/// <typeparam name="TResponse">The type of the output response.</typeparam>
public interface IUseCase<TRequest, TResponse>
{
    /// <summary>
    /// Executes the use case with the given request.
    /// </summary>
    /// <param name="request">The input request to process.</param>
    /// <returns>The response resulting from processing the request.</returns>
    TResponse Execute(TRequest request);
}