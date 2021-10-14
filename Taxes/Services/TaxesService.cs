using System;
using System.Threading.Tasks;

namespace Taxes.Services
{
    internal sealed class TaxesService
    {
        private readonly ExchangeRatesService _exchangeRatesService;

        public TaxesService()
        {
            _exchangeRatesService = new ExchangeRatesService();
        }
        public async Task<decimal> CalculateTaxesAsync(decimal income, DateTime incomeDate)
        {
            var exchangeRate = await _exchangeRatesService.GetExchangeRateAsync(incomeDate);
            return Math.Round(exchangeRate * income * 0.05M, 2, MidpointRounding.ToPositiveInfinity);
        } 
    }
}
