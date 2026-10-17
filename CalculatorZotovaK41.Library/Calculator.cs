using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculatorZotovaK41.Library
{
    public class Calculator
    {
        public double Compute(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentException("Пустое выражение");
            }

            // Убираем пробелы для надежности
            string cleanExpression = expression.Replace(" ", "");

            // Проверка на явное деление на ноль
            if (cleanExpression.Contains("/0") && !cleanExpression.Contains("/0."))
            {
                throw new DivideByZeroException("Деление на ноль невозможно");
            }

            // Обработка степеней (поддерживает сложные выражения, точки и запятые)
            if (cleanExpression.Contains("^"))
            {
                cleanExpression = ProcessPowers(cleanExpression);
            }

            // ВАЖНО: Передаем выражение в DataTable КАК ЕСТЬ (с точкой), 
            // так как DataTable не понимает запятые как десятичный разделитель.
            var table = new DataTable();

            try
            {
                var result = table.Compute(cleanExpression, "");
                return Convert.ToDouble(result);
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException("Деление на ноль невозможно");
            }
            catch
            {
                throw new Exception("Ошибка вычисления выражения");
            }
        }

        private string ProcessPowers(string expression)
        {
            // Регулярное выражение ищет и точку, и запятую в числах: [.,]
            Regex regex = new Regex(@"(-?\d+(?:[.,]\d+)?)\^(-?\d+(?:[.,]\d+)?)");

            while (regex.IsMatch(expression))
            {
                expression = regex.Replace(expression, match =>
                {
                    // Заменяем запятую на точку для правильного парсинга
                    string baseStr = match.Groups[1].Value.Replace(',', '.');
                    string expStr = match.Groups[2].Value.Replace(',', '.');

                    double baseValue = double.Parse(baseStr, CultureInfo.InvariantCulture);
                    double exponent = double.Parse(expStr, CultureInfo.InvariantCulture);

                    double result = Math.Pow(baseValue, exponent);

                    // Возвращаем результат с точкой (для DataTable)
                    return result.ToString(CultureInfo.InvariantCulture);
                }, 1);
            }

            return expression;
        }
    }
}