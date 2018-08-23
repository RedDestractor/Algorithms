using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    public class Test
    {
        [Test]
        public void TestSimple()
        {
            var dataMock = new List<String>
            {
                "Ben Alexander",
                "Alexander Dolly",
                "Dolly Ben",
                "Dolly Benedict",
                "Benedict Dolly",
                "Alexander Aaron",
                ""
            };

            var callinCirles = new CallingCircles(new ConsoleWrapperTest(dataMock));

            var result = callinCirles.GetCallCircles();

            Assert.AreEqual("Benedict, Dolly, Alexander, Ben\r\n", result);
        }

        [Test]
        public void TestHard()
        {
            var dataMock = new List<String>
            {
                "John Aaron",
                "Aaron Benedict",
                "Betsy John",
                "Betsy Ringo",
                "Ringo Dolly",
                "Benedict Paul",
                "John Betsy",
                "John Aaron",
                "Benedict George",
                "Dolly Ringo",
                "Paul Martha",
                "George Ben",
                "Alexander George",
                "Betsy Ringo",
                "Alexander Stephen",
                "Martha Stephen",
                "Benedict Alexander",
                "Stephen Paul",
                "Betsy Ringo",
                "Quincy Martha",
                "Ben Patrick",
                "Betsy Ringo",
                "Patrick Stephen",
                "Paul Alexander",
                "Patrick Ben",
                "Ringo Betsy",
                "Betsy Benedict",
                "Betsy Benedict",
                "Betsy Benedict",
                "Betsy Benedict",
                "Quincy Martha",
                ""
            };

            var callinCirles = new CallingCircles(new ConsoleWrapperTest(dataMock));

            var result = callinCirles.GetCallCircles();

            Assert.AreEqual("Patrick, Ben, George, Alexander, Stephen, Martha, Paul\r\n" +
                "Dolly, Ringo, Betsy, John\r\n" , result);
        }
    }

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
}
