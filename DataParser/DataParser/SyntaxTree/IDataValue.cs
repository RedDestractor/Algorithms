using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataParser.SyntaxTree
{
    public interface IDataValue
    {
        int Id { get; set; }
        int IdParent { get; set; }
        string Name { get; set; }
        string Value { get; set; }
    }
}
