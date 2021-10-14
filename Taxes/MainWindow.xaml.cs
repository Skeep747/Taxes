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
        private readonly TaxesService _taxesService;

        public MainWindow()
        {
            _taxesService = new TaxesService();

            InitializeComponent();
            IncomeDate.SelectedDate = DateTime.Today;
        }

        private async void CalculateTaxes_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Income.Text, out decimal income) && IncomeDate.SelectedDate.HasValue)
            {
                Taxes.Content = await _taxesService.CalculateTaxesAsync(income, IncomeDate.SelectedDate.Value);
            }
        }
    }
}
