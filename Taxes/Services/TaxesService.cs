using Serilog;
using System;
using System.Threading.Tasks;

namespace Taxes.Services
{
    public interface ITaxesService
    {
        Task<decimal> CalculateTaxesAsync(decimal income, DateTime incomeDate);
    }

    public sealed class TaxesService : ITaxesService
    {
        private readonly ILogger _logger;
        private readonly IExchangeRatesService _exchangeRatesService;

        public TaxesService(ILogger logger, IExchangeRatesService exchangeRatesService)
        {
            _logger = logger;
            _exchangeRatesService = exchangeRatesService;
        }

        public async Task<decimal> CalculateTaxesAsync(decimal income, DateTime incomeDate)
        {
            var exchangeRate = await _exchangeRatesService.GetExchangeRateAsync(incomeDate);
            return Math.Round(exchangeRate * income * 0.05M, 2, MidpointRounding.ToPositiveInfinity);
        } 
    }
}
