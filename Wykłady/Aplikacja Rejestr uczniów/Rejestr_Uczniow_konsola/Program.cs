using System;
using System.IO;

namespace RejestrUczniow
{
    class Program
    {
        static string[] imieUcznia = new string[100];
        static string[] nazwiskoUcznia = new string[100];
        static int[,] stopnie = new int[100, 3];
        static int liczbaZapisanych = 0;
        static string sciezkaPliku = "dane.txt";

        static void Main(string[] args)
        {
            bool czyKontynuowac = true;

            do
            {
                Console.WriteLine("\n--- GLOWNE MENU ---");
                Console.WriteLine("[1] Odczyt danych z pliku txt");
                Console.WriteLine("[2] Rejestracja nowego ucznia");
                Console.WriteLine("[3] Pokaz liste i statystyki");
                Console.WriteLine("[4] Zapis i zamkniecie programu");
                Console.Write("Wybierz opcje (1-4): ");

                string decyzja = Console.ReadLine();
                Console.WriteLine();

                switch (decyzja)
                {
                    case "1":
                        PobierzZPliku();
                        break;
                    case "2":
                        WprowadzUcznia();
                        break;
                    case "3":
                        WypiszInformacje();
                        break;
                    case "4":
                        ZapiszDoPliku();
                        czyKontynuowac = false;
                        break;
                    default:
                        Console.WriteLine("Blad: Wybrano nieistniejaca opcje!");
                        break;
                }
            } while (czyKontynuowac);
        }

        static void PobierzZPliku()
        {
            if (!File.Exists(sciezkaPliku))
            {
                Console.WriteLine("Uwaga: Brak pliku z danymi. Baza jest pusta.");
                return;
            }

            StreamReader plikOdczyt = new StreamReader(sciezkaPliku);
            string wiersz;
            liczbaZapisanych = 0;

            while ((wiersz = plikOdczyt.ReadLine()) != null)
            {
                if (liczbaZapisanych >= 100) break;

                string[] daneWiersza = wiersz.Split(' ');

                if (daneWiersza.Length == 5)
                {
                    imieUcznia[liczbaZapisanych] = daneWiersza[0];
                    nazwiskoUcznia[liczbaZapisanych] = daneWiersza[1];
                    stopnie[liczbaZapisanych, 0] = Convert.ToInt32(daneWiersza[2]);
                    stopnie[liczbaZapisanych, 1] = Convert.ToInt32(daneWiersza[3]);
                    stopnie[liczbaZapisanych, 2] = Convert.ToInt32(daneWiersza[4]);
                    liczbaZapisanych++;
                }
            }
            plikOdczyt.Close();
            Console.WriteLine("Sukces: Zaladowano " + liczbaZapisanych + " osob z pliku.");
        }

        static void WprowadzUcznia()
        {
            if (liczbaZapisanych == 100)
            {
                Console.WriteLine("Limit osiagniety (100). Nie mozna dodac wiecej osob.");
                return;
            }

            Console.Write("Wpisz imie: ");
            imieUcznia[liczbaZapisanych] = Console.ReadLine();

            Console.Write("Wpisz nazwisko: ");
            nazwiskoUcznia[liczbaZapisanych] = Console.ReadLine();

            int licznikOcen = 0;
            while (licznikOcen < 3)
            {
                Console.Write("Ocena nr " + (licznikOcen + 1) + ": ");
                stopnie[liczbaZapisanych, licznikOcen] = Convert.ToInt32(Console.ReadLine());
                licznikOcen++;
            }

            liczbaZapisanych++;
            Console.WriteLine("Informacja: Uczen poprawnie dopisany do listy.");
        }

        static double WyliczSrednia(int indeks)
        {
            double sumaOcen = stopnie[indeks, 0] + stopnie[indeks, 1] + stopnie[indeks, 2];
            return sumaOcen / 3.0;
        }

        static void WypiszInformacje()
        {
            if (liczbaZapisanych == 0)
            {
                Console.WriteLine("Brak danych do wyswietlenia.");
                return;
            }

            double sumaSrednich = 0.0;

            Console.WriteLine("=== LISTA UCZNIOW ===");
            for (int k = 0; k < liczbaZapisanych; k++)
            {
                double aktualnaSrednia = WyliczSrednia(k);
                sumaSrednich += aktualnaSrednia;

                Console.WriteLine((k + 1) + ". " + imieUcznia[k] + " " + nazwiskoUcznia[k] +
                                  " | Oceny: " + stopnie[k, 0] + ", " + stopnie[k, 1] + ", " + stopnie[k, 2] +
                                  " | Srednia: " + Math.Round(aktualnaSrednia, 2));
            }

            double wynikKlasy = sumaSrednich / liczbaZapisanych;
            Console.WriteLine("\n=== PODSUMOWANIE ===");
            Console.WriteLine("Zarejestrowanych uczniow: " + liczbaZapisanych);
            Console.WriteLine("Ogolna srednia ocen: " + Math.Round(wynikKlasy, 2));
        }

        static void ZapiszDoPliku()
        {
            StreamWriter plikZapis = new StreamWriter(sciezkaPliku);
            for (int j = 0; j < liczbaZapisanych; j++)
            {
                plikZapis.WriteLine(imieUcznia[j] + " " + nazwiskoUcznia[j] + " " +
                                    stopnie[j, 0] + " " + stopnie[j, 1] + " " + stopnie[j, 2]);
            }
            plikZapis.Close();
            Console.WriteLine("Zapis zakonczony. Program zostanie wylaczony.");
        }
    }
}
