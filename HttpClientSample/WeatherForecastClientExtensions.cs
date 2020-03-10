using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace HttpClientSample
{
    public static class WeatherForecastClientExtensions
    {
        private static readonly Random Jitterer = new Random();
        private const int RetryCount = 5;

        public static IHttpClientBuilder AddWeatherForecastClient(this IServiceCollection services, Uri baseAddress)
        {
            return services
                .AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(httpClient =>
                {
                    httpClient.BaseAddress = baseAddress;
                    httpClient.DefaultRequestHeaders.Add("Weather-Forecast-Api-Key-Name", "WeatherForecastApiKeyValue");
                })
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(
                        RetryCount,
                        currentRetryNumber => TimeSpan.FromSeconds(Math.Pow(2, currentRetryNumber)) + TimeSpan.FromMilliseconds(Jitterer.Next(0, 100)),
                        (currentException, currentSleepDuration, currentRetryNumber, currentContext) => 
                        {
                            if (currentRetryNumber == RetryCount)
                            {
                                throw currentException.Exception;
                            }
#if DEBUG
                            Debug.WriteLine($"=== Attempt {currentRetryNumber} ===");
                            Debug.WriteLine(nameof(currentException) + ": " + currentException.Exception);
                            Debug.WriteLine(nameof(currentSleepDuration) + ": " + currentSleepDuration);
#endif
                        }));
        }
    }
}
