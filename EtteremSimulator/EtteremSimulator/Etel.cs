using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Etel
    {
        public string Nev { get; }
        public int ElkeszitesiIdo { get; }

        public Etel(string nev, int elkeszitesiIdo)
        {
            Nev = nev;
            ElkeszitesiIdo = elkeszitesiIdo;
        }
    }
}
