using System;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClientSample
{
    public static class WeatherForecastClientExtensions
    {
        public static IHttpClientBuilder AddWeatherForecastClient(this IServiceCollection services, Uri baseAddress)
        {
            return services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(httpClient =>
            {
                httpClient.BaseAddress = baseAddress;
                httpClient.DefaultRequestHeaders.Add("Weather-Forecast-Api-Key-Name", "WeatherForecastApiKeyValue");
            });
        }
    }
}
