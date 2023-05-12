using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Windows;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	/// Camilo Mahecha
	public sealed partial class CurrencyConverter : Page
	{

		decimal amountConversion = 0;


		public CurrencyConverter()
		{
			this.InitializeComponent();
		}


		private async void ConversionButton(object sender, RoutedEventArgs e)
		{


			try
			{
				amountConversion = (decimal)double.Parse(AmountTextBox.Text);

			}
			catch (Exception theException)
			{

				var dialogMessage = new MessageDialog("Please enter a valid amount, " + theException.Message);
				await dialogMessage.ShowAsync();
				AmountTextBox.Focus(FocusState.Programmatic);

				return;
			}

			if (amountConversion <= 0)
			{
				var dialogMessage = new MessageDialog("Must be greater than cero");
				await dialogMessage.ShowAsync();
				AmountTextBox.Focus(FocusState.Programmatic);
				AmountTextBox.Text = "";
				return;
			}

			{





				double fromAmount = double.Parse(AmountTextBox.Text);
				string fromCurrency = ((ComboBoxItem)fromCurrencyComboBox.SelectedItem).Content.ToString();
				string toCurrency = ((ComboBoxItem)toCurrencyComboBox.SelectedItem).Content.ToString();
				double rate = GetConversionRate(fromCurrency, toCurrency);
				double rate2 = GetConversionRate(toCurrency, fromCurrency);

				double toAmount = fromAmount * rate;

				string subfromCurrency = fromCurrency.Substring(2);
				string subtoCurrency = toCurrency.Substring(2);

				toAmountTextBox.Text = fromAmount + " " + subfromCurrency + " = ";
				toAmountTextBox2.Text = toAmount.ToString("C") + " " + subtoCurrency + "s";
				toAmountTextBox3.Text = "1 " + subfromCurrency + " = " + rate.ToString("C") + " " + subtoCurrency;
				toAmountTextBox4.Text = "1 " + subtoCurrency + " = " + rate2.ToString("C") + " " + subfromCurrency;

			}


		}



		private double GetConversionRate(string fromCurrency, string toCurrency)
		{
			switch (fromCurrency)
			{
				case "$ US Dollar":
					switch (toCurrency)
					{
						case "€ Euro":
							return 0.85189982;
						case "£ British Pound":
							return 0.72872436;
						case "₹ Indian Rupee":
							return 74.257327;
					}
					break;
				case "€ Euro":
					switch (toCurrency)
					{
						case "$ US Dollar":
							return 1.1739732;
						case "£ British Pound":
							return 0.8556672;
						case "₹ Indian Rupee":
							return 87.00755;
					}
					break;
				case "£ British Pound":
					switch (toCurrency)
					{
						case "$ US Dollar":
							return 1.371907;
						case "€ Euro":
							return 1.1686692;
						case "₹ Indian Rupee":
							return 101.68635;
					}
					break;
				case "₹ Indian Rupee":
					switch (toCurrency)
					{
						case "$ US Dollar":
							return 0.011492628;
						case "€ Euro":
							return 0.013492774;
						case "£ British Pound":
							return 0.0098339397;
					}
					break;
			}
			return 0;
		}





		private void ExitClickButton(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Menu));
		}

		private void From_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem comboBoxItem = (ComboBoxItem)fromCurrencyComboBox.SelectedItem;
			string imagePath = (string)comboBoxItem.Tag;
			image01.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

		}
	}
}
