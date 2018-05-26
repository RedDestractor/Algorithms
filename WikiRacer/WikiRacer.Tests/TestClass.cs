using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using NUnit.Framework;
using Sprache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using WikiRacer;

namespace WikiRacer.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void test_file_parsing_without_logic()
        {
            var stream = FileManager.GetFileFromResource("textio/[output]-simple.txt");
            var streamResult = FileManager.GetFileFromResource("textio/_output.txt");
            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (StreamReader streamReaderResult = new StreamReader(streamResult))
                {
                    string input;
                    string inputResult;
                    while ((input = streamReader.ReadLine()) != null)
                    {
                        if ((inputResult = streamReaderResult.ReadLine()) == null)
                            Assert.Fail();
                        if (inputResult != input)
                            Assert.Fail();
                    }
                    Assert.Pass();
                }
            }
        }

        [Test]
        public void test_simple_parse()
        {
            var parser = new HtmlParser();
            var document = parser.Parse("<p>The <b>sea otter</b> (<i>Enhydra lutris</i>) is a <a href=\"/wiki" +
                                        "/Marine_mammal\" title=\"Marine mammal\">marine mammal</a><p>The <b>sea otter</b>" +
                                        " (<i>Enhydra lutris</i>) is a <a href=\"/Marine_mammal\" title=\"Marine mammal\">wrong</a>");

        }
    }
}
