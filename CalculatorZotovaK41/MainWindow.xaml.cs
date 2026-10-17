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

        // Обработчик для цифр и операторов
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            DisplayTextBox.Text += b.Content.ToString();
        }

        // Оранжевая кнопка Del (удаляет по одному символу)
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayTextBox.Text.Length > 0)
            {
                DisplayTextBox.Text = DisplayTextBox.Text.Substring(0, DisplayTextBox.Text.Length - 1);
            }
        }

        // Красная кнопка C (полностью очищает экран и историю)
        private void R_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "";
            HistoryTextBlock.Text = "";
        }

        // Кнопка "=" (Результат)
        private void Result_click(object sender, RoutedEventArgs e)
        {
            // Объявляем expression ЗДЕСЬ, чтобы она была видна и в try, и в catch
            string expression = DisplayTextBox.Text;

            try
            {
                if (string.IsNullOrWhiteSpace(expression)) return;

                // Вычисляем
                var result = _calculator.Compute(expression);

                // ЗАПИСЫВАЕМ В ИСТОРИЮ (пример = ответ)
                HistoryTextBlock.Text = expression + " = " + result.ToString();

                // Показываем только ответ в основном поле
                DisplayTextBox.Text = result.ToString();
            }
            catch (DivideByZeroException)
            {
                HistoryTextBlock.Text = expression + " = Ошибка";
                DisplayTextBox.Text = "Деление на ноль!";
            }
            catch (Exception)
            {
                HistoryTextBlock.Text = expression + " = Ошибка";
                DisplayTextBox.Text = "Ошибка ввода";
            }
        }
    }
}