using Serilog;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Taxes.CustomExceptions;

namespace Taxes.Services
{
    public interface IExchangeRatesService
    {
        Task<decimal> GetExchangeRateAsync(DateTime date);
    }

    public sealed class ExchangeRatesService : IExchangeRatesService
    {
        private class ExchangeRatesResponse
        {
            public int r030 { get; set; }
            public string? txt { get; set; }
            public decimal rate { get; set; }
            public string? cc { get; set; }
            public string? exchangedate { get; set; }
        }

        private readonly ILogger _logger;

        public ExchangeRatesService(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<decimal> GetExchangeRateAsync(DateTime date)
        {
            var result = 0M;
            try
            {
                var client = new HttpClient();
                var url = string.Format("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&date={0}&json", date.ToString("yyyyMMdd"));
                string responseBody = await client.GetStringAsync(url);
                var exchangeRatesResponse = JsonSerializer.Deserialize<ExchangeRatesResponse[]>(responseBody);
                if (exchangeRatesResponse == null || !exchangeRatesResponse.Any())
                {
                    throw new ExchangeRateResponseException(responseBody);
                }
                result = exchangeRatesResponse.First().rate;
            }
            catch (HttpRequestException e)
            {
                _logger.Error("Something went wrong when calling the api https://bank.gov.ua", e);
            }
            catch (ExchangeRateResponseException e)
            {
                _logger.Error("Failed to deserialize response", e);
            }
            catch (Exception e)
            {
                _logger.Error("Unhandled exception", e);
            }

            return result;
        }
    }
}
