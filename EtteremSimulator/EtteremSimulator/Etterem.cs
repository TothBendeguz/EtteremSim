using System;
using System.Collections.Generic;
using System.Threading;

namespace EtteremSimulator
{
    internal class Etterem
    {
        private Asztal[] asztalok;
        private Pincer[] pincerek;
        private Szakacs[] szakacsok;
        private List<Etel> menu;
        private string aktualisSzezon;

        public List<Etel> Menu { get { return menu; } }

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

            aktualisSzezon = "Általános";
            menu = new List<Etel>
            {
                new Etel("Húsleves", 15),
                new Etel("Sült csirke", 20),
                new Etel("Spagetti", 18),
                new Etel("Pizza", 25)
            };
        }

        public void SzezonValtas(string szezon)
        {
            aktualisSzezon = szezon;
            menu.Clear();
            switch (szezon)
            {
                case "Tavasz":
                    menu.Add(new Etel("Tavaszi saláta", 10, "Tavasz"));
                    menu.Add(new Etel("Rántott hús", 22, "Tavasz"));
                    break;
                case "Nyár":
                    menu.Add(new Etel("Grillezett csirke", 18, "Nyár"));
                    menu.Add(new Etel("Jégkrém", 5, "Nyár"));
                    break;
                case "Ősz":
                    menu.Add(new Etel("Gombaleves", 12, "Ősz"));
                    menu.Add(new Etel("Sült pulyka", 30, "Ősz"));
                    break;
                case "Tél":
                    menu.Add(new Etel("Halászlé", 20, "Tél"));
                    menu.Add(new Etel("Töltött káposzta", 25, "Tél"));
                    break;
                default:
                    menu.Add(new Etel("Húsleves", 15));
                    menu.Add(new Etel("Sült csirke", 20));
                    menu.Add(new Etel("Spagetti", 18));
                    menu.Add(new Etel("Pizza", 25));
                    break;
            }
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

                if (asztal.Vendeg == null && !asztal.Foglalt)
                {
                    asztal.Vendeg = new Vendeg(rnd.Next(1, 5));
                    asztal.Foglalt = true;
                    Console.WriteLine($"Asztal {asztal.Szam} üres. Új vendég érkezett: {asztal.Vendeg.Szam} fő.");
                }

                // Rendelés felvétele
                if (asztal.Vendeg != null && !asztal.Vendeg.Rendelt)
                {
                    Etel rendeles = menu[rnd.Next(menu.Count)];
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
                        asztalok[j].Takarit();
                    }
                }

                Thread.Sleep(1000); // Szimulációs lassítás
            }
        }

        public bool AsztalFoglalas(int foszam)
        {
            for (int i = 0; i < asztalok.Length; i++)
            {
                if (!asztalok[i].Foglalt)
                {
                    asztalok[i].Vendeg = new Vendeg(foszam);
                    asztalok[i].Foglalt = true;
                    return true;
                }
            }
            return false;
        }

        public void AsztalTakaritas()
        {
            for (int i = 0; i < asztalok.Length; i++)
            {
                if (asztalok[i].Foglalt && asztalok[i].Vendeg == null)
                {
                    asztalok[i].Takarit();
                }
            }
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