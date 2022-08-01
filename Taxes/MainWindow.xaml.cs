using Serilog;
using System;
using System.Windows;
using Taxes.Services;

namespace Taxes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        private readonly ITaxesService _taxesService;

        public MainWindow(ILogger logger, ITaxesService taxesService)
        {
            _logger = logger;
            _taxesService = taxesService;

            InitializeComponent();
            IncomeDate1.SelectedDate = DateTime.Today;
            IncomeDate2.SelectedDate = DateTime.Today;
            IncomeDate3.SelectedDate = DateTime.Today;
        }

        private async void CalculateTaxes_Click(object sender, RoutedEventArgs e)
        {
            decimal total = 0;

            if (decimal.TryParse(Income1.Text, out decimal income1) && IncomeDate1.SelectedDate.HasValue)
            {
                var tax = await _taxesService.CalculateTaxesAsync(income1, IncomeDate1.SelectedDate.Value);
                if (tax > 0)
                {
                    var taxPlusExtraValue = AddExtraValue(tax, ExtraValue1.Text);
                    total += taxPlusExtraValue;
                    Taxes1.Content = taxPlusExtraValue + "₴";
                }
                else
                {
                    TaxesTotal.Content = "Something went wrong! Please, see the log file for more information.";
                    return;
                }
            }
            else
            {
                Taxes1.Content = string.Empty;
            }

            if (decimal.TryParse(Income2.Text, out decimal income2) && IncomeDate2.SelectedDate.HasValue)
            {
                var tax = await _taxesService.CalculateTaxesAsync(income2, IncomeDate2.SelectedDate.Value);
                if (tax > 0)
                {
                    var taxPlusExtraValue = AddExtraValue(tax, ExtraValue2.Text);
                    total += taxPlusExtraValue;
                    Taxes2.Content = taxPlusExtraValue + "₴";
                }
                else
                {
                    TaxesTotal.Content = "Something went wrong! Please, see the log file for more information.";
                    return;
                }
            }
            else
            {
                Taxes2.Content = string.Empty;
            }

            if (decimal.TryParse(Income3.Text, out decimal income3) && IncomeDate3.SelectedDate.HasValue)
            {
                var tax = await _taxesService.CalculateTaxesAsync(income3, IncomeDate3.SelectedDate.Value);
                if (tax > 0)
                {
                    var taxPlusExtraValue = AddExtraValue(tax, ExtraValue3.Text);
                    total += taxPlusExtraValue;
                    Taxes3.Content = taxPlusExtraValue + "₴";
                }
                else
                {
                    TaxesTotal.Content = "Something went wrong! Please, see the log file for more information.";
                    return;
                }
            }
            else
            {
                Taxes3.Content = string.Empty;
            }

            TaxesTotal.Content = total + "₴";
        }

        private static decimal AddExtraValue(decimal tax, string extraValue)
        {
            if (decimal.TryParse(extraValue, out decimal extra))
            {
                return tax + extra;
            }
            return tax;
        }
    }
}
