using System;
using System.Data;
using System.Globalization;

namespace CalculatorZotovaK41.Library
{
    public class Calculator
    {
        public double Compute(string expression)
        {
            // Проверка на деление на ноль
            if (expression.Contains("/0"))
            {
                throw new DivideByZeroException("Деление на ноль невозможно");
            }

            // Обработка степени (если есть ^)
            if (expression.Contains("^"))
            {
                return EvaluatePower(expression);
            }

            // Для операций +, -, *, / заменяем точку на запятую для DataTable
            string expressionForTable = expression.Replace('.', ',');
            var table = new DataTable();
            var result = table.Compute(expressionForTable, "");
            return Convert.ToDouble(result);
        }

        private double EvaluatePower(string expression)
        {
            // Разделяем выражение по знаку ^
            string[] parts = expression.Split('^');

            if (parts.Length != 2)
            {
                throw new ArgumentException("Некорректное выражение со степенью");
            }

            // Парсим числа с поддержкой точки как разделителя
            double baseValue = double.Parse(parts[0], CultureInfo.InvariantCulture);
            double exponent = double.Parse(parts[1], CultureInfo.InvariantCulture);

            return Math.Pow(baseValue, exponent);
        }
    }
}




