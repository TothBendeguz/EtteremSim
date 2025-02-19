using System;
using System.Collections.Generic;

namespace EtteremSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Etterem etterem = new Etterem();
            Menu(etterem);
        }

        static void Menu(Etterem etterem)
        {
            string[] options = { "Szimuláció indítása", "Asztal foglalása", "Étlap megtekintése", "Vendégek listázása", "Szezonális menü választása", "Kilépés" };
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Étterem Menü ===");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        HandleSelection(selectedIndex, etterem);
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }

        static void HandleSelection(int index, Etterem etterem)
        {
            Console.Clear();
            switch (index)
            {
                case 0:
                    InditSzimulacio(etterem);
                    break;
                case 1:
                    AsztalFoglalas(etterem);
                    break;
                case 2:
                    EtlapMegtekintese(etterem);
                    break;
                case 3:
                    VendegListazas(etterem);
                    break;
                case 4:
                    SzezonValtas(etterem);
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("\nNyomj egy gombot a folytatáshoz...");
            Console.ReadKey();
        }

        static void InditSzimulacio(Etterem etterem)
        {
            Console.Write("Hány iterációt szeretnél szimulálni? ");
            if (int.TryParse(Console.ReadLine(), out int iteraciok) && iteraciok > 0)
            {
                etterem.Indit(iteraciok);
            }
            else
            {
                Console.WriteLine("Érvénytelen bemenet!");
            }
        }

        static void AsztalFoglalas(Etterem etterem)
        {
            Console.Write("Hány fős asztalt szeretnél foglalni? ");
            if (int.TryParse(Console.ReadLine(), out int foszam) && foszam > 0)
            {
                if (etterem.AsztalFoglalas(foszam))
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
                Console.WriteLine("Érvénytelen bemenet!");
            }
        }

        static void EtlapMegtekintese(Etterem etterem)
        {
            Console.WriteLine("=== Étlap ===");
            foreach (var etel in etterem.Menu)
            {
                Console.WriteLine($"{etel.Nev} - {etel.ElkeszitesiIdo} perc");
            }
        }

        static void VendegListazas(Etterem etterem)
        {
            Console.WriteLine("=== Vendégek ===");
            var vendegLista = etterem.VendegLista();
            if (vendegLista.Count > 0)
            {
                foreach (var par in vendegLista)
                {
                    Console.WriteLine($"Asztal {par.Key}: {par.Value.Szam} fő");
                }
            }
            else
            {
                Console.WriteLine("Nincsenek vendégek.");
            }
        }

        

        static void SzezonValtas(Etterem etterem)
        {
            string[] szezonok = { "Tavasz", "Nyár", "Ősz", "Tél" };
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Szezonális Menü Választása ===");
                for (int i = 0; i < szezonok.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(szezonok[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + szezonok.Length) % szezonok.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % szezonok.Length;
                        break;
                    case ConsoleKey.Enter:
                        etterem.SzezonValtas(szezonok[selectedIndex]);
                        Console.WriteLine($"Szezonális menü beállítva: {szezonok[selectedIndex]}");
                        return;
                }
            } while (key != ConsoleKey.Escape);
        }
    }
}