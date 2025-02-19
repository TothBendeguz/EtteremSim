using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtteremSimulator
{
    internal class UI
    {
        private Etterem etterem;

        public UI(Etterem etterem)
        {
            this.etterem = etterem;
        }

        public void Menu()
        {
            bool fut = true;
            while (fut)
            {
                Console.Clear();
                Console.WriteLine("=== Étterem Menü ===");
                Console.WriteLine("1. Szimuláció indítása");
                Console.WriteLine("2. Asztal foglalása");
                Console.WriteLine("3. Étlap megtekintése");
                Console.WriteLine("4. Vendégek listázása");
                Console.WriteLine("5. Kilépés");
                Console.Write("Válassz egy opciót: ");

                string valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                        SzimulacioInditas();
                        break;
                    case "2":
                        AsztalFoglalas();
                        break;
                    case "3":
                        EtlapMegtekintese();
                        break;
                    case "4":
                        VendegListazas();
                        break;
                    case "5":
                        Console.WriteLine("Kilépés...");
                        fut = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás, próbáld újra!");
                        break;
                }

                if (fut)
                {
                    Console.WriteLine("\nNyomj egy gombot a folytatáshoz...");
                    Console.ReadKey();
                }
            }
        }

        private void SzimulacioInditas()
        {
            Console.Write("Hány iterációt szeretnél szimulálni? ");
            if (int.TryParse(Console.ReadLine(), out int iteraciok) && iteraciok > 0)
            {
                etterem.Indit(iteraciok);
            }
            else
            {
                Console.WriteLine("Érvénytelen bemenet, pozitív egész számot adj meg!");
            }
        }

        private void AsztalFoglalas()
        {
            Console.Write("Hány fős asztalt szeretnél foglalni? ");
            if (int.TryParse(Console.ReadLine(), out int foszam) && foszam > 0)
            {
                bool siker = etterem.AsztalFoglalas(foszam);
                if (siker)
                {
                    Console.WriteLine("Asztal sikeresen foglalva!");
                }
                else
                {
                    Console.WriteLine("Nincs szabad asztal.");
                }
            }
            else
            {
                Console.WriteLine("Érvénytelen bemenet, pozitív egész számot adj meg!");
            }
        }

        private void EtlapMegtekintese()
        {
            Console.WriteLine("=== Étlap ===");
            Etel[] etlap = etterem.Menu;
            for (int i = 0; i < etlap.Length; i++)
            {
                Console.WriteLine($"{etlap[i].Nev} - {etlap[i].ElkeszitesiIdo} perc");
            }
        }

        private void VendegListazas()
        {
            Console.WriteLine("=== Vendégek ===");
            Dictionary<int, Vendeg> vendegLista = etterem.VendegLista();
            if (vendegLista.Count > 0)
            {
                foreach (KeyValuePair<int, Vendeg> par in vendegLista)
                {
                    Console.WriteLine($"Asztal {par.Key}: {par.Value.Szam} fő");
                }
            }
            else
            {
                Console.WriteLine("Nincsenek vendégek.");
            }
        }
    }
}
