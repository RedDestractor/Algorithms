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
        public void TestSimple()
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

        [Test]
        public void TestHard()
        {
            var dataMock = new List<String>();
            dataMock.Add("John Aaron");
            dataMock.Add("Aaron Benedict");
            dataMock.Add("Betsy John");
            dataMock.Add("Betsy Ringo");
            dataMock.Add("Ringo Dolly");
            dataMock.Add("Benedict Paul");
            dataMock.Add("John Betsy");
            dataMock.Add("John Aaron");
            dataMock.Add("Benedict George");
            dataMock.Add("Dolly Ringo");
            dataMock.Add("Paul Martha");
            dataMock.Add("George Ben");
            dataMock.Add("Alexander George");
            dataMock.Add("Betsy Ringo");
            dataMock.Add("Alexander Stephen");
            dataMock.Add("Martha Stephen");
            dataMock.Add("Benedict Alexander");
            dataMock.Add("Stephen Paul");
            dataMock.Add("Betsy Ringo");
            dataMock.Add("Quincy Martha");
            dataMock.Add("Ben Patrick");
            dataMock.Add("Betsy Ringo");
            dataMock.Add("Patrick Stephen");
            dataMock.Add("Paul Alexander");
            dataMock.Add("Patrick Ben");
            dataMock.Add("Ringo Betsy");
            dataMock.Add("Betsy Benedict");
            dataMock.Add("Betsy Benedict");
            dataMock.Add("Betsy Benedict");
            dataMock.Add("Betsy Benedict");
            dataMock.Add("Quincy Martha");
            dataMock.Add("");

            var callinCirles = new CallingCircles(new ConsoleWrapperTest(dataMock));

            var result = callinCirles.GetCallCircles();

            Assert.AreEqual("Patrick, Ben, George, Alexander, Stephen, Martha, Paul\r\n" +
                "Dolly, Ringo, Betsy, John\r\n" , result);
        }
    }
}
