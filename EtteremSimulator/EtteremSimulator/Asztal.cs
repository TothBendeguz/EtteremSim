using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Asztal
    {
        public int Szam { get; }
        public Vendeg Vendeg { get; set; }

        public Asztal(int szam)
        {
            Szam = szam;
        }
    }
}
