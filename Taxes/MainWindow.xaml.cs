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
            IncomeDate.SelectedDate = DateTime.Today;
        }

        private async void CalculateTaxes_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Income.Text, out decimal income) && IncomeDate.SelectedDate.HasValue)
            {
                var tax = await _taxesService.CalculateTaxesAsync(income, IncomeDate.SelectedDate.Value);
                if (tax > 0)
                {
                    Taxes.Content = tax;
                }
                else
                {
                    Taxes.Content = "Something went wrong! Please, see the log file for more information.";
                }
            }
            else
            {
                Taxes.Content = "The value must be a number, and the date must be selected!";
            }
        }
    }
}
