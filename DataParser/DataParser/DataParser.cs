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
                    (next == 'f') ? '\f' :
                    (next == 'b') ? '\b' :
                    (next == 'r') ? '\r' :
                    (next == 'n') ? '\n' :
                    next);

        static readonly Parser<IDataLiteral> DataName =
            from first in Parse.Letter.Or(Parse.Char('_')).Once()
            from rest in AllowedChar.Many()
            select new DataLiteral(new string(first.Concat(rest).ToArray()));

        static readonly Parser<IDataValue> DataObject =
            from name in DataName
            from colon in Parse.Char('=').Token()
            from _first in Parse.Char('{').Token()
            from value in Parse.Ref(() => PairKeyValueMembers)
            from _last in Parse.Char('}').Token()
            select new DataKeyValuePair(name, new DataArray(value));

        static readonly Parser<IDataLiteral> DataLiteral =
            from first in Parse.Char('"')
            from value in Parse.AnyChar.Except(Parse.Char('"')).Or(ControlChar).Or(Parse.Char('\\')).Many().Text()
            from last in Parse.Char('"')
            select new DataLiteral(value);

        static readonly Parser<IDataValue> PairKeyArray =
            from name in DataName
            from colon in Parse.Char('=').Token()
            from value in DataArray
            select new DataKeyValuePair(name, value);

        static readonly Parser<IDataValue> PairKeyLiteral =
            from name in DataName
            from colon in Parse.Char('=').Token()
            from value in DataLiteral.Or(Parse.Ref(() => PairKeyArray))
            select new DataKeyValuePair(name, value);

        static readonly Parser<IDataValue> DataArray =
            from _first in Parse.Char('[').Token()
            from elements in DataRow
            from _last in Parse.Char(']').Token()
            select new DataArray(elements);

        static readonly Parser<IEnumerable<IDataValue>> PairKeyValueMembers =
            DataObject.Or(PairKeyLiteral).Or(PairKeyArray).DelimitedBy(Parse.Char(',').Token());

        static readonly Parser<IEnumerable<IDataValue>> DataRow = PairKeyLiteral.DelimitedBy(Parse.WhiteSpace);

        public static IDataValue ParseData(string toParse)
        {
            return DataObject.Parse(toParse);
        }
    }
}
