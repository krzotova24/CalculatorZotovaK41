using System;
using System.Windows;
using System.Windows.Controls;
using CalculatorZotovaK41.Library;

namespace CalculatorZotovaK41
{
    public partial class MainWindow : Window
    {
        private Calculator _calculator = new Calculator();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            DisplayTextBox.Text += b.Content.ToString();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayTextBox.Text.Length > 0)
            {
                DisplayTextBox.Text = DisplayTextBox.Text.Substring(0, DisplayTextBox.Text.Length - 1);
            }
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "";
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string expression = DisplayTextBox.Text;
                var result = _calculator.Compute(expression);
                DisplayTextBox.Text = result.ToString();
            }
            catch
            {
                DisplayTextBox.Text = "Error";
            }
        }

        private double Compute(string expression)
        {
            try
            {
                expression = expression.Replace("^", "↑"); 
                while (expression.Contains("↑"))
                {
                    int pos = expression.IndexOf("↑");
                    int leftStart = pos - 1;
                    while (leftStart >= 0 && (char.IsDigit(expression[leftStart]) || expression[leftStart] == '.'))
                    {
                        leftStart--;
                    }
                    leftStart++;
                    int rightEnd = pos + 1;
                    while (rightEnd < expression.Length && (char.IsDigit(expression[rightEnd]) || expression[rightEnd] == '.'))
                    {
                        rightEnd++;
                    }

                    string leftNumStr = expression.Substring(leftStart, pos - leftStart);
                    string rightNumStr = expression.Substring(pos + 1, rightEnd - pos - 1);

                    double leftNum = Convert.ToDouble(leftNumStr);
                    double rightNum = Convert.ToDouble(rightNumStr);

                    double powerResult = Math.Pow(leftNum, rightNum);

                    expression = expression.Substring(0, leftStart) +
                                 powerResult.ToString() +
                                 expression.Substring(rightEnd);
                }

                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(expression, "");
                return Convert.ToDouble(result);
            }
            catch
            {
                throw new Exception("Ошибка вычисления");
            }
        }
    }
}



