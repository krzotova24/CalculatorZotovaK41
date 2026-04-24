using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorZotovaK41.Library;

namespace CalculatorZotovaK41.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestMethod]
        public void Сложение_5Плюс7_Возвращает12()
        {
            double результат = _calculator.Compute("5+7");
            Assert.AreEqual(12, результат);
        }

        [TestMethod]
        public void Вычитание_20Минус8_Возвращает12()
        {
            double результат = _calculator.Compute("20-8");
            Assert.AreEqual(12, результат);
        }

        [TestMethod]
        public void Умножение_4УмножитьНа9_Возвращает36()
        {
            double результат = _calculator.Compute("4*9");
            Assert.AreEqual(36, результат);
        }

        [TestMethod]
        public void Деление_100РазделитьНа4_Возвращает25()
        {
            double результат = _calculator.Compute("100/4");
            Assert.AreEqual(25, результат);
        }

        [TestMethod]
        public void ПриоритетУмножения_3Плюс5УмножитьНа2_Возвращает13()
        {
            double результат = _calculator.Compute("3+5*2");
            Assert.AreEqual(13, результат);
        }

        [TestMethod]
        public void СложноеВыражение_10Минус3УмножитьНа2Плюс1_Возвращает5()
        {
            double результат = _calculator.Compute("10-3*2+1");
            Assert.AreEqual(5, результат);
        }

        [TestMethod]
        public void Степень_3ВСтепени2_Возвращает9()
        {
            double результат = _calculator.Compute("3^2");
            Assert.AreEqual(9, результат);
        }

        [TestMethod]
        public void СтепеньСДробью_9ВСтепени0Точка5_Возвращает3()
        {
            double результат = _calculator.Compute("9^0.5");
            Assert.AreEqual(3, результат);
        }

        [TestMethod]
        public void ДелениеНаНоль_10РазделитьНа0_ВыбрасываетИсключение()
        {
            // Act & Assert
            Assert.ThrowsException<DivideByZeroException>(() => _calculator.Compute("10/0"));
        }
    }
}




