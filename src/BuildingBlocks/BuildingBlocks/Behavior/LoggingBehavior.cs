using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behavior;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={requestName} - Response={responseName} - RequestData={data}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var (response, elapsedTimeInSeconds) = await ExecuteRequest(next, cancellationToken);

        if (elapsedTimeInSeconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] The request {request} took {elapsedTime} " +
                "seconds and a review may be necessary. Request={requestData} - " +
                "Response={responseData}",
                typeof(TRequest).Name, elapsedTimeInSeconds, request, response);
        }

        logger.LogInformation("[END] Handled request {request} with response {response}",
            request, response);

        return response;
    }

    private async Task<(TResponse response, int elapsedTimeInSeconds)> ExecuteRequest(
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();

        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();

        return (response, timer.Elapsed.Seconds);
    }
}
