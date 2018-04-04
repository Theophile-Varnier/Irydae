using System.Collections.Generic;
using Irydae.Model;

namespace Irydae.Helpers
{
    public class PartenaireEqualityComparer : IEqualityComparer<Partenaire>
    {
        public bool Equals(Partenaire x, Partenaire y)
        {
            return x.Nom == y.Nom;
        }

        public int GetHashCode(Partenaire obj)
        {
            return obj.Nom.GetHashCode();
        }
    }
}