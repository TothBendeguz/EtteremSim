using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Szakacs
    {
        public int Szam { get; }

        public Szakacs(int szam)
        {
            Szam = szam;
        }

        public void EtelKeszit(Etel etel)
        {
            Thread.Sleep(etel.ElkeszitesiIdo * 100); // Szimuláljuk az étel elkészítésének idejét
        }
    }
}
