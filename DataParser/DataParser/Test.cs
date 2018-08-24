using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser
{
    class Test
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("TestFile.txt"))
            {
                var parsed = DataParser.ParseData(reader.ReadToEnd());
            }
        }
    }
}
