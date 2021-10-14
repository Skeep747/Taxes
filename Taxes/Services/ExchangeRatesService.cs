using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Taxes.Services
{
    internal sealed class ExchangeRatesService
    {
        private class ExchangeRatesResponse
        {
            public int r030 { get; set; }
            public string? txt { get; set; }
            public decimal rate { get; set; }
            public string? cc { get; set; }
            public string? exchangedate { get; set; }
        }

        public async Task<decimal> GetExchangeRateAsync(DateTime date)
        {
            try
            {
                var client = new HttpClient();
                var url = string.Format("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=USD&date={0}&json", date.ToString("yyyyMMdd"));
                string responseBody = await client.GetStringAsync(url);
                var exchangeRatesResponse = JsonSerializer.Deserialize<ExchangeRatesResponse[]>(responseBody);
                if (exchangeRatesResponse == null)
                {
                    throw new Exception(responseBody);
                }
                return exchangeRatesResponse.First().rate;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
