using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.App
{
    public partial class MainWindow : Window
    {
        private CalculatorManager calculator = new CalculatorManager();
        private bool calculationFinished;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var value = button.Content.ToString();

            if (!int.TryParse(value, out int tmp))
            {
                calculationFinished = false;
            }
            if (calculationFinished)
            {
                textBox.Text = value;
                calculationFinished = false;
            }
            else
            {
                textBox.Text += value;
            }
        }

        private void Button_GetResult(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = calculator.GetCalculation(textBox.Text);
                textBox.Text = result.ToString();
                calculationFinished = true;
            }
            catch (Exception)
            {
                textBox.Text = "Wrong input, please try again";
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && !calculator.AllowedOperations.Contains(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

