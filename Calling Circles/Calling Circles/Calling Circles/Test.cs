using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    public class ConsoleWrapperTest : IConsole
    {
        public List<String> linesToRead;

        public ConsoleWrapperTest(List<String> input)
        {
            linesToRead = input;
        }

        public void Write(string message)
        {
        }

        public void WriteLine(string message)
        {
        }

        public string ReadLine()
        {
            string result = linesToRead[0];
            linesToRead.RemoveAt(0);
            return result;
        }
    }

    public class Test
    {
        [Test]
        public void TestSimpleOne()
        {
            var dataMock = new List<String>();
            dataMock.Add("Ben Alexander");
            dataMock.Add("Alexander Dolly");
            dataMock.Add("Dolly Ben");
            dataMock.Add("Dolly Benedict");
            dataMock.Add("Benedict Dolly");
            dataMock.Add("Alexander Aaron");
            dataMock.Add("");
            var callinCirles = new CallingCircles(new ConsoleWrapperTest(dataMock));

            var result = callinCirles.GetCallCircles();

            Assert.AreEqual("Benedict, Dolly, Alexander, Ben\r\n", result);
        }
    }
}
