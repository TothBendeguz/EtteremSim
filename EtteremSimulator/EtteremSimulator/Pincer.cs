using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Pincer
    {
        public int Szam { get; }
        private Asztal[] rendelesek;
        private int rendelesekSzama;

        public Pincer(int szam)
        {
            Szam = szam;
            rendelesek = new Asztal[10];
            rendelesekSzama = 0;
        }

        public void RendelesFelvesz(Asztal asztal)
        {
            if (rendelesekSzama < rendelesek.Length)
            {
                rendelesek[rendelesekSzama] = asztal;
                rendelesekSzama++;
            }
        }

        public Asztal RendelesLead()
        {
            if (rendelesekSzama > 0)
            {
                Asztal asztal = rendelesek[0];
                for (int i = 1; i < rendelesekSzama; i++)
                {
                    rendelesek[i - 1] = rendelesek[i];
                }
                rendelesekSzama--;
                return asztal;
            }
            return null;
        }

        public int RendelesekSzama()
        {
            return rendelesekSzama;
        }
    }
}
