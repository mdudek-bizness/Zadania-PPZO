using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;


public class Figura
{
    public double Pole;
    public double Obwod;

    public virtual void PobierzDane()
    {
        Console.WriteLine("Pobieranie danych");
    }

    public virtual void WyswietlDane()
    {
        Console.WriteLine("===== Wyniki =====");
        Console.WriteLine("Pole: " + Pole);
        Console.WriteLine("Obwód: " + Obwod);
        Console.WriteLine("==================");
    }

    public virtual double ObliczObwod()
    {
        return 0.0;
    }

    public virtual double ObliczPole()
    {
        return 0.0;
    }
}

    public class Kwadrat : Figura
    {
        public double bok;

        public override void PobierzDane()
        {
            Console.Write("Podaj dlugosc boku kwadratu: ");
            bok = Convert.ToDouble(Console.ReadLine());
        }

        public override double ObliczObwod()
        {
            Obwod = 4 * bok;
            return Obwod;
        }

        public override double ObliczPole()
        {
            Pole = bok * bok;
            return Pole;
        }

        public override void WyswietlDane()
        {
            Console.WriteLine("\n===== Kwadrat =====");
            Console.WriteLine("Bok: " + bok);
            Console.WriteLine("Obwod: " + Obwod);
            Console.WriteLine("Pole: " + Pole);
        }
    }

    public class Prostokat : Figura
    {
        public double bokA;
        public double bokB;

        public override void PobierzDane()
        {
            Console.Write("Podaj dlugosc boku A prostokata: ");
            bokA = Convert.ToDouble(Console.ReadLine());
            Console.Write("Podaj dlugosc boku B prostokata: ");
            bokB = Convert.ToDouble(Console.ReadLine());
        }

        public override double ObliczObwod()
        {
            Obwod = (2 * bokA) + (2 * bokB);
            return Obwod;
        }

        public override double ObliczPole()
        {
            Pole = bokA * bokB;
            return Pole;
        }

        public override void WyswietlDane()
        {
            Console.WriteLine("\n===== Prostokat =====");
            Console.WriteLine("Bok A: " + bokA);
            Console.WriteLine("Bok B: " + bokB);
            Console.WriteLine("Obwod: " + Obwod);
            Console.WriteLine("Pole: " + Pole);
        }

    }

    public class Kolo : Figura
    {
        public double promien;

        public override void PobierzDane()
        {
            Console.Write("Podaj promien kola: ");
            promien = Convert.ToDouble(Console.ReadLine());
        }

        public override double ObliczObwod()
        {
            Obwod = 2 * Math.PI * promien;
            return Obwod;
        }

        public override double ObliczPole()
        {
            Pole = Math.PI * promien * promien;
            return Pole;
        }

        public override void WyswietlDane()
        {
            Console.WriteLine("\n===== Kolo =====");
            Console.WriteLine("Promien: " + promien);
            Console.WriteLine("Obwod: " + Obwod);
            Console.WriteLine("Pole: " + Pole);
        }
    }

    public class Trojkat : Figura
    {
        public double bokA;
        public double bokB;
        public double bokC;

        public override void PobierzDane()
        {
            Console.Write("Podaj bok A trojkata: ");
            bokA = Convert.ToDouble(Console.ReadLine());
            Console.Write("Podaj bok B trojkata: ");
            bokB = Convert.ToDouble(Console.ReadLine());
            Console.Write("Podaj bok C trojkata: ");
            bokC = Convert.ToDouble(Console.ReadLine());
        }

        public override double ObliczObwod()
        {
            Obwod = bokA + bokB + bokC;
            return Obwod;
        }

        public override double ObliczPole()
        {
            //ze wzoru Herona
            double p = ObliczObwod() / 2;
            Pole = Math.Sqrt(p * (p - bokA) * (p - bokB) * (p - bokC));
            return Pole;
        }

        public override void WyswietlDane()
        {
            Console.WriteLine("\n===== Trojkat =====");
            Console.WriteLine("Boki: " + bokA + ", " + bokB + ", " + bokC);
            Console.WriteLine("Obwod: " + Obwod);
            Console.WriteLine("Pole: " + Pole);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList listaFigur = new ArrayList();

            int wybor = -1;

            while (wybor != 0)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. - Dodaj Kwadrat");
                Console.WriteLine("2. - Dodaj Prostokat");
                Console.WriteLine("3. - Dodaj Kolo");
                Console.WriteLine("4. - Dodaj Trojkat");
                Console.WriteLine("5. - Wyswietl wszystkie figury z listy");
                Console.WriteLine("6. - Zapisz dane do pliku");
                Console.WriteLine("7. - Odczytaj dane z pliku");
                Console.WriteLine("0. - Wyjscie z programu");
                Console.Write("Wybierz opcje: ");

                wybor = Convert.ToInt32(Console.ReadLine());

                Figura nowaFigura = null;

                switch (wybor)
                {
                    case 1:
                        nowaFigura = new Kwadrat();
                        break;
                    case 2:
                        nowaFigura = new Prostokat();
                        break;
                    case 3:
                        nowaFigura = new Kolo();
                        break;
                    case 4:
                        nowaFigura = new Trojkat();
                        break;
                    case 5:
                        Console.WriteLine("\n===== Lista Figur =====");

                        foreach (Figura f in listaFigur)
                        {
                            f.WyswietlDane();
                        }
                        break;
                    case 6:
                        ZapiszDoPliku(listaFigur);
                        break;
                    case 7:
                        OdczytajZPliku(listaFigur);
                        break;
                    case 0:
                        Console.WriteLine("Koniec programu.");
                        break;
                    default:
                        Console.WriteLine("Niepoprawaty wybor opcji");
                        break;
                }

                if (wybor >= 1 && wybor <= 4 && nowaFigura != null)
                {
                    nowaFigura.PobierzDane();
                    nowaFigura.ObliczPole();
                    nowaFigura.ObliczObwod();

                    listaFigur.Add(nowaFigura);
                    Console.WriteLine("Figura zostala dodana do listy.");
                }
            }
        }



        static void ZapiszDoPliku(ArrayList lista)
        {
            string sciezka = "figury.txt";

            using (StreamWriter sw = new StreamWriter(sciezka))
            {
                foreach (Figura f in lista)
                {
                    if (f is Kwadrat)
                    {
                        Kwadrat k = (Kwadrat)f;
                        sw.WriteLine("Kwadrat;" + k.bok);
                    }
                    else if (f is Prostokat)
                    {
                        Prostokat p = (Prostokat)f;
                        sw.WriteLine("Prostokat;" + p.bokA + ";" + p.bokB);
                    }
                    else if (f is Kolo)
                    {
                        Kolo ko = (Kolo)f;
                        sw.WriteLine("Kolo;" + ko.promien);
                    }
                    else if (f is Trojkat)
                    {
                        Trojkat t = (Trojkat)f;
                        sw.WriteLine("Trojkat;" + t.bokA + ";" + t.bokB + ";" + t.bokC);
                    }
                }
            }
            Console.WriteLine("Dane zostaly zapisane do pliku: " + sciezka);
        }

        static void OdczytajZPliku(ArrayList lista)
        {
            string sciezka = "figury.txt";

            if (!File.Exists(sciezka))
            {
                Console.WriteLine("Plik z danymi nie istnieje/jest w zlej lokalizacji");
                return;
            }

            lista.Clear();

            using (StreamReader sr = new StreamReader(sciezka))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    string[] czesci = linia.Split(";");
                    string typ = czesci[0];

                    Figura wyczytana = null;

                    if (typ == "Kwadrat")
                    {
                        Kwadrat k = new Kwadrat();
                        k.bok = Convert.ToDouble(czesci[1]);
                        wyczytana = k;
                    }
                    else if (typ == "Prostokat")
                    {
                        Prostokat p = new Prostokat();
                        p.bokA = Convert.ToDouble(czesci[1]);
                        p.bokB = Convert.ToDouble(czesci[2]);
                        wyczytana = p;
                    }
                    else if (typ == "Kolo")
                    {
                        Kolo ko = new Kolo();
                        ko.promien = Convert.ToDouble(czesci[1]);
                        wyczytana = ko;
                    }
                    else if (typ == "Trojkat")
                    {
                        Trojkat t = new Trojkat();
                        t.bokA = Convert.ToDouble(czesci[1]);
                        t.bokB = Convert.ToDouble(czesci[2]);
                        t.bokC = Convert.ToDouble(czesci[3]);
                        wyczytana = t;
                    }

                    if (wyczytana != null)
                    {
                        wyczytana.ObliczPole();
                        wyczytana.ObliczObwod();
                        lista.Add(wyczytana);
                    }
                }
            }
            Console.WriteLine("Dane zostaly odczytane z pliku i zaladowane do listy");
        }
    }