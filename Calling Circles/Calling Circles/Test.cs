using NUnit.Framework;
using System.Collections.Generic;

namespace Calling_Circles
{
    public class Test
    {
        [Test]
        public void TestSimple()
        {
            var dataMock = new List<string>
            {
                "Ben Alexander",
                "Alexander Dolly",
                "Dolly Ben",
                "Dolly Benedict",
                "Benedict Dolly",
                "Alexander Aaron",
                ""
            };

            var result = new CallingCircles(new ConsoleWrapperTest(dataMock)).GetCallCircles();

            Assert.AreEqual("Benedict, Dolly, Alexander, Ben\r\n", result);
        }

        [Test]
        public void TestHard()
        {
            var dataMock = new List<string>
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

            var result = new CallingCircles(new ConsoleWrapperTest(dataMock)).GetCallCircles();

            Assert.AreEqual("Patrick, Ben, George, Alexander, Stephen, Martha, Paul\r\n" +
                "Dolly, Ringo, Betsy, John\r\n" , result);
        }
    }

    public class ConsoleWrapperTest : IConsole
    {
        public List<string> LinesToRead { get; set; }

        public ConsoleWrapperTest(List<string> input)
        {
            LinesToRead = input;
        }

        public void Write(string message)
        {
        }

        public void WriteLine(string message)
        {
        }

        public string ReadLine()
        {
            var result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }
    }
}
