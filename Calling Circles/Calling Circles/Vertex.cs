using System.Collections.Generic;

namespace Calling_Circles
{
    public class Vertex
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int Lowlink { get; set; }
        public string Name { get; set; }
        public HashSet<Vertex> Neighbors { get; }

        public Vertex()
        {
            Id = -1;
            Index = -1;
            Lowlink = -1;
            Name = null;
            Neighbors = new HashSet<Vertex>();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Vertex other))
                return false;

            return Id == other.Id && Name == other.Name;
        }
    }
}
