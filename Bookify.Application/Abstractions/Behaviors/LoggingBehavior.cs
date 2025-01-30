using Bookify.Application.Abstractions.Messaging;
using MediatR;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bookify.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestData = JsonSerializer.Serialize(request);
        var stopwatch = Stopwatch.StartNew();

        logger.LogInformation("Handling command: {RequestName} with data: {RequestData}",
            requestName, requestData);

        try
        {
            var response = await next(); 
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 15000)
            {
                logger.LogWarning("Command {RequestName} took {ElapsedMilliseconds} ms to execute, which exceeds the 15-second threshold!",
                    requestName, stopwatch.ElapsedMilliseconds);
            }

            logger.LogInformation("Command {RequestName} handled successfully in {ElapsedMilliseconds} ms with response: {ResponseData}",
                requestName, stopwatch.ElapsedMilliseconds, JsonSerializer.Serialize(response));

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.LogError(ex, "Command {RequestName} failed after {ElapsedMilliseconds} ms with error: {ErrorMessage}",
                requestName, stopwatch.ElapsedMilliseconds, ex.Message);
            throw;
        }
    }
}