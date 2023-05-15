using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}
		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			// Total ammount to borrow
			double borrowAmountTotal;
			// Payment years of payments
			double paymentYears;
			// Total months of payments
			double paymentMonths;
			// Yearly interest rate input
			double yearlyInterestRate;
			// Yearly interest rate converted to monthly
			double monthlyInterestRate;
			// Total payment months + years as months
			double paymentDuration;
			// Open Monthly repayments total
			double monthlyRepayments;
			// Math.Pow() first
			double mathFirst;
			// Math.Pow() second
			double mathSecond;
			// Finalise interest rate (divide by 100 to get a percentage from a whole number)
			double totalInterestRate;

			if (string.IsNullOrEmpty(borrowAmountBox.Text))
			{
				var dialog = new MessageDialog("Borrow Ammount Error Cannot Be Left Blank");
				await dialog.ShowAsync();
				borrowAmountBox.Focus(FocusState.Programmatic);
				return;
			}

			if (string.IsNullOrEmpty(yearsTextBox.Text))
			{
				yearsTextBox.Text = "0";
			}

			if (string.IsNullOrEmpty(monthsTextBox.Text))
			{
				monthsTextBox.Text = "0";
			}

			if (string.IsNullOrEmpty(yearlyInterestAmountBox.Text))
			{
				var dialog = new MessageDialog("Yearly Interest Rate Error Cannot Be Left Blank");
				await dialog.ShowAsync();
				yearlyInterestAmountBox.Focus(FocusState.Programmatic);
				return;
			}

			try
			{
				// Total borrow amount
				borrowAmountTotal = double.Parse(borrowAmountBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Borrow Amount Error " + exception.Message + " Please Enter A Price Amount");
				await dialog.ShowAsync();
				borrowAmountBox.Focus(FocusState.Programmatic);
				return;
			}

			try
			{
				// Payment years
				paymentYears = double.Parse(yearsTextBox.Text);
			}
			catch (Exception exception)
			{
				var dialog = new MessageDialog("Payment Years Error " + exception.Message + " Please Enter Whole Numbers Only");
				await dialog.ShowAsync();
				yearsTextBox.Focus(FocusState.Programmatic);
				return;
			}

			try
			{
				// Payment Months
				paymentMonths = double.Parse(monthsTextBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Payment Months Error " + exception.Message + " Please Enter Whole Numbers Only");
				await dialog.ShowAsync();
				monthsTextBox.Focus(FocusState.Programmatic);
				return;
			}

			try
			{
				yearlyInterestRate = double.Parse(yearlyInterestAmountBox.Text);
			}

			catch (Exception exception)
			{
				var dialog = new MessageDialog("Interest Rate Error " + exception.Message + " Please Enter as a decimal");
				await dialog.ShowAsync();
				yearlyInterestAmountBox.Focus(FocusState.Programmatic);
				return;
			}
			paymentDuration = paymentMonths + (paymentYears * 12);
			monthlyInterestRate = yearlyInterestRate / 12;

			totalInterestRate = monthlyInterestRate / 100;

			mathFirst = Math.Pow(1 + totalInterestRate, paymentDuration);
			mathSecond = Math.Pow(1 + totalInterestRate, paymentDuration - 1);

			monthlyRepayments = borrowAmountTotal * (totalInterestRate * mathFirst) / mathSecond;

			monthlyInterestRateBox.Text = monthlyInterestRate.ToString();
			monthlyRepaymentOutput.Text = monthlyRepayments.ToString("C");
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(Menu));
		}
	}
}
