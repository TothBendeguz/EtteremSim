using System;

namespace EtteremSimulator
{
    internal class Etel
    {
        public string Nev { get; }
        public int ElkeszitesiIdo { get; }
        public string Szezon { get; }

        public Etel(string nev, int elkeszitesiIdo, string szezon = "Általános")
        {
            Nev = nev;
            ElkeszitesiIdo = elkeszitesiIdo;
            Szezon = szezon;
        }
    }
}