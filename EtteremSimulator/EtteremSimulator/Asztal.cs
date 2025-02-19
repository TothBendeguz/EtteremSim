using System;

namespace EtteremSimulator
{
    internal class Asztal
    {
        public int Szam { get; }
        public Vendeg Vendeg { get; set; }
        public bool Foglalt { get; set; }

        public Asztal(int szam)
        {
            Szam = szam;
            Foglalt = false;
        }

        public void Takarit()
        {
            Vendeg = null;
            Foglalt = false;
        }
    }
}