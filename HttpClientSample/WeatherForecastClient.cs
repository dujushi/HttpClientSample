using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class WeatherForecastClient : IWeatherForecastClient
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;

        public WeatherForecastClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default)
        {
            var weatherForecastString = JsonSerializer.Serialize(weatherForecast, JsonSerializerOptions);
            var stringContent = new StringContent(weatherForecastString, Encoding.UTF8, MediaTypeNames.Application.Json);
            AddRequestHeader("Weather-Forecast-Header-Name", "WeatherForecastHeaderValue");
            await _httpClient.PostAsync("r/1fwlgic1", stringContent, cancellationToken);
        }

        private void AddRequestHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Remove(name);
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }
    }
}
