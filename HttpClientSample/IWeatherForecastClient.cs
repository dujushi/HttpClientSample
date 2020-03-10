using System.Threading;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public interface IWeatherForecastClient
    {
        Task AddWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default);
    }
}
