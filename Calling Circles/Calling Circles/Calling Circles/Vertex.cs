using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calling_Circles
{
    public class Vertex
    {
        public int id;
        public int index;
        public int lowlink;
        public string name;
        public HashSet<Vertex> neighbors;

        public Vertex()
        {
            id = -1;
            index = -1;
            lowlink = -1;
            name = null;
            neighbors = new HashSet<Vertex>();
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() ^ name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Vertex other))
                return false;

            return id == other.id && name == other.name;
        }
    }
}
