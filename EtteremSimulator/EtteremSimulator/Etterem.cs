using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class Etterem
    {
        private Asztal[] asztalok;
        private Pincer[] pincerek;
        private Szakacs[] szakacsok;
        private Etel[] menu;

        public Etel[] Menu { get { return menu; } }

        public Etterem()
        {
            asztalok = new Asztal[5];
            for (int i = 0; i < asztalok.Length; i++)
            {
                asztalok[i] = new Asztal(i + 1);
            }

            pincerek = new Pincer[2];
            for (int i = 0; i < pincerek.Length; i++)
            {
                pincerek[i] = new Pincer(i + 1);
            }

            szakacsok = new Szakacs[2];
            for (int i = 0; i < szakacsok.Length; i++)
            {
                szakacsok[i] = new Szakacs(i + 1);
            }

            menu = new Etel[]
            {
            new Etel("Húsleves", 15),
            new Etel("Sült csirke", 20),
            new Etel("Spagetti", 18),
            new Etel("Pizza", 25)
            };
        }

        public void Indit(int iteraciok)
        {
            Random rnd = new Random();

            for (int i = 0; i < iteraciok; i++)
            {
                Console.WriteLine($"\n--- Iteráció {i + 1} ---");

                // Vendégek érkezése
                int asztalSzam = rnd.Next(asztalok.Length);
                Asztal asztal = asztalok[asztalSzam];

                if (asztal.Vendeg == null)
                {
                    asztal.Vendeg = new Vendeg(rnd.Next(1, 5));
                    Console.WriteLine($"Asztal {asztal.Szam} üres. Új vendég érkezett: {asztal.Vendeg.Szam} fő.");
                }

                // Rendelés felvétele
                if (asztal.Vendeg != null && !asztal.Vendeg.Rendelt)
                {
                    Etel rendeles = menu[rnd.Next(menu.Length)];
                    asztal.Vendeg.Rendel(rendeles);
                    Console.WriteLine($"Asztal {asztal.Szam} rendelt: {rendeles.Nev}");

                    Pincer pincer = pincerek[rnd.Next(pincerek.Length)];
                    pincer.RendelesFelvesz(asztal);
                }

                // Pincérek és szakácsok dolgoznak
                for (int j = 0; j < pincerek.Length; j++)
                {
                    if (pincerek[j].RendelesekSzama() > 0)
                    {
                        Szakacs szakacs = szakacsok[rnd.Next(szakacsok.Length)];
                        Asztal asztal1 = pincerek[j].RendelesLead();
                        szakacs.EtelKeszit(asztal1.Vendeg.Rendeles);
                        asztal1.Vendeg.EtelFogyaszt();
                        Console.WriteLine($"Asztal {asztal1.Szam} megkapta a rendelést: {asztal1.Vendeg.Rendeles.Nev}");
                    }
                }

                // Vendégek elégedettségének ellenőrzése
                for (int j = 0; j < asztalok.Length; j++)
                {
                    if (asztalok[j].Vendeg != null && asztalok[j].Vendeg.EtelFogyasztva)
                    {
                        Console.WriteLine($"Asztal {asztalok[j].Szam} vendégei elégedettek: {asztalok[j].Vendeg.Elegedettseg}%");
                        asztalok[j].Vendeg = null;
                    }
                }

                Thread.Sleep(1000); // Szimulációs lassítás
            }
        }

        public bool AsztalFoglalas(int foszam)
        {
            for (int i = 0; i < asztalok.Length; i++)
            {
                if (asztalok[i].Vendeg == null)
                {
                    asztalok[i].Vendeg = new Vendeg(foszam);
                    return true;
                }
            }
            return false;
        }

        public Dictionary<int, Vendeg> VendegLista()
        {
            Dictionary<int, Vendeg> vendegLista = new Dictionary<int, Vendeg>();
            for (int i = 0; i < asztalok.Length; i++)
            {
                if (asztalok[i].Vendeg != null)
                {
                    vendegLista.Add(asztalok[i].Szam, asztalok[i].Vendeg);
                }
            }
            return vendegLista;
        }
    }
}
