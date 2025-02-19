using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Vendeg
    {
        public int Szam { get; }
        public Etel Rendeles { get; private set; }
        public bool Rendelt { get; private set; }
        public bool EtelFogyasztva { get; private set; }
        public int Elegedettseg { get; private set; }

        public Vendeg(int szam)
        {
            Szam = szam;
            Elegedettseg = 100;
        }

        public void Rendel(Etel etel)
        {
            Rendeles = etel;
            Rendelt = true;
        }

        public void EtelFogyaszt()
        {
            EtelFogyasztva = true;
            Elegedettseg -= new Random().Next(0, 20);
        }
    }
}
