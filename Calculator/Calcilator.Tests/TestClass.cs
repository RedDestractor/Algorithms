using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calcilator;
using Calculator.App;

namespace Calcilator.Tests
{
    [TestFixture]
    public class TestClass
    {
        CalculatorManager calculator = new CalculatorManager();
        [Test]
        public void simple_calculation_numbers_with_whitespace_true()
        {
            var result = calculator.GetCalculation("1 + 1");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void simple_calculation_numbers_without_whitespace_true()
        {
            var result = calculator.GetCalculation("1+1");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void simple_calculation_numbers_without_whitespace_true_2()
        {
            var result = calculator.GetCalculation("1+1/4*(48+10)");
            Assert.AreEqual(15.5, result);
        }

        [Test]
        public void calculation_fractional_numbers_true()
        {
            var result = calculator.GetCalculation("0,111111111*5");
            Assert.AreEqual(0.55555, result);
        }
    }
}
