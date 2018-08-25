using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataParser.SyntaxTree;
using Sprache;

namespace DataParser
{
    public static class DataParser
    {
        static List<char> EscapeChars = new List<char> { '\"', '\\', 'b', 'f', 'n', 'r', 't' };
        static readonly Parser<char> AllowedChar = Parse.LetterOrDigit.Or(Parse.Char('_'));

        static Parser<U> EnumerateInput<T, U>(T[] input, Func<T, Parser<U>> parser)
        {
            if (input == null || input.Length == 0) throw new ArgumentNullException("input");
            if (parser == null) throw new ArgumentNullException("parser");

            return i =>
            {
                foreach (var inp in input)
                {
                    var res = parser(inp)(i);
                    if (res.WasSuccessful) return res;
                }

                return Result.Failure<U>(null, null, null);
            };
        }

        static readonly Parser<char> ControlChar =
            from first in Parse.Char('\\')
            from next in EnumerateInput(EscapeChars.ToArray(), c => Parse.Char(c))
            select ((next == 't') ? '\t' :
                    (next == 'r') ? '\r' :
                    (next == 'n') ? '\n' :
                    (next == 'f') ? '\f' :
                    (next == 'b') ? '\b' :
                    next);

        static readonly Parser<string> DataName =
            from first in Parse.Letter.Or(Parse.Char('_')).Once()
            from rest in AllowedChar.Many()
            select new string(first.Concat(rest).ToArray());

        static readonly Parser<IDataValue> DataObject =
            from name in DataName
            from colon in Parse.Char('=').Or(Parse.WhiteSpace).Many()
            from _first in Parse.Char('{').Or(Parse.WhiteSpace).Many().Token()
            from value in DataMembers
            from _last in Parse.Char('}').Or(Parse.WhiteSpace).Many().Token()
            select new DataKeyValuePair(new DataLiteral(name), new DataArray(value));

        static readonly Parser<IDataLiteral> DataValue =
            from first in Parse.Char('"')
            from value in Parse.AnyChar.Except(Parse.Char('"').Or(ControlChar)).Many().Text()
            from last in Parse.Char('"')
            select new DataLiteral(value);

        static readonly Parser<IDataValue> DataArray =
            from name in DataName
            from colon in Parse.Char('=').Token()
            from _ in Parse.Char('{').Or(Parse.WhiteSpace).Many().Token()
            from elements in DataRow
            from last in Parse.Char('}').Or(Parse.WhiteSpace).Many().Token()
            select new DataArray(elements);

        static readonly Parser<IDataValue> DataPair =
            from name in DataName
            from colon in Parse.Char('=').Token()
            from val in DataValue
            select new DataKeyValuePair(new DataLiteral(name), val);

        static readonly Parser<IEnumerable<IDataValue>> DataObjects = DataObject.DelimitedBy(Parse.LineEnd);

        static readonly Parser<IEnumerable<IDataValue>> DataMembers = DataArray.DelimitedBy(Parse.LineEnd).Or(DataPair.DelimitedBy(Parse.LineEnd));

        static readonly Parser<IEnumerable<IDataValue>> DataRow = DataPair.DelimitedBy(Parse.WhiteSpace);        

        public static IEnumerable<IDataValue> ParseData(string toParse)
        {
            return DataObjects.Parse(toParse);
        }
    }
}
