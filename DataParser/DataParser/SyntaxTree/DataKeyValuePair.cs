﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.SyntaxTree
{
    class DataKeyValuePair : IDataValue
    {
        public IDataLiteral Key { get; set; }
        public IDataLiteral Value { get; set; }

        public DataKeyValuePair(IDataLiteral name, IDataLiteral value)
        {
            Key = name;
            Value = value;
        }
    }
}
