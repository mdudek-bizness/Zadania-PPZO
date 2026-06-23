using System;
using System.Collections.Generic;
using System.IO;

abstract class Parcela
{

    public int Numer { get; private set; }
    public string Nazwa { get; private set; }
    public double MaxDlugoscPojazdu { get; private set; }
    public decimal CenaZaDobe { get; private set; }

    public Parcela(int numer, string nazwa, double maxDlugosc, decimal cena)
    {
        Numer = numer;
        Nazwa = nazwa;
        MaxDlugoscPojazdu = maxDlugosc;
        CenaZaDobe = cena;
    }


    public abstract void PokazOpis();
}

class ParcelaStandard : Parcela
{
    public ParcelaStandard(int numer) : base(numer, "Standard", 8.0, 60m) { }

    public override void PokazOpis()
    {
        Console.WriteLine($"[{Numer}] {Nazwa} (30m2, woda/prąd). Max pojazd: {MaxDlugoscPojazdu}m. Cena: {CenaZaDobe} zł/doba");
    }
}

class ParcelaPremium : Parcela
{
    public ParcelaPremium(int numer) : base(numer, "Premium", 13.0, 100m) { }

    public override void PokazOpis()
    {
        Console.WriteLine($"[{Numer}] {Nazwa} (60m2, prysznic, WC). Max pojazd: {MaxDlugoscPojazdu}m. Cena: {CenaZaDobe} zł/doba");
    }
}

class ParcelaDeluxe : Parcela
{

    public ParcelaDeluxe(int numer) : base(numer, "Deluxe", double.MaxValue, 250m) { }

    public override void PokazOpis()
    {
        Console.WriteLine($"[{Numer}] {Nazwa} (100m2, basen, jezioro). Max pojazd: Bez limitu. Cena: {CenaZaDobe} zł/doba");
    }
}


class Rezerwacja
{
    public int Id { get; private set; }

    public Parcela ZarezerwowanaParcela { get; private set; }
    public DateTime DataPrzyjazdu { get; private set; }
    public DateTime DataOdjazdu { get; private set; }

    public Rezerwacja(int id, Parcela parcela, DateTime przyjazd, DateTime odjazd)
    {
        Id = id;
        ZarezerwowanaParcela = parcela;
        DataPrzyjazdu = przyjazd;
        DataOdjazdu = odjazd;
    }

    public void WypiszSzczegoly()
    {
        Console.WriteLine($"ID: {Id} | Parcela: {ZarezerwowanaParcela.Numer} ({ZarezerwowanaParcela.Nazwa}) | Od: {DataPrzyjazdu:dd/MM/yyyy} Do: {DataOdjazdu:dd/MM/yyyy}");
    }
}
class SystemRezerwacji
{
    private List<Parcela> parcele;
    private List<Rezerwacja> rezerwacje;
    private int nastepneIdRezerwacji = 1;

    public SystemRezerwacji()
    {
        parcele = new List<Parcela>();
        rezerwacje = new List<Rezerwacja>();
        InicjalizujParcele();
    }

    private void InicjalizujParcele()
    {
        for (int i = 1; i <= 9; i++) parcele.Add(new ParcelaStandard(i));
        for (int i = 10; i <= 19; i++) parcele.Add(new ParcelaPremium(i));
        for (int i = 20; i <= 25; i++) parcele.Add(new ParcelaDeluxe(i));
    }

    public void WyswietlParcele()
    {
        Console.WriteLine("\n--- DOSTĘPNE RODZAJE PARCELI ---");
        parcele[0].PokazOpis();  // Standard
        parcele[9].PokazOpis();  // Premium
        parcele[19].PokazOpis(); // Deluxe
    }

    public void Rezerwuj(string standard, double dlugosc, DateTime przyjazd, DateTime odjazd)
    {
        string poszukiwanaNazwa = "";
        if (standard == "1") poszukiwanaNazwa = "Standard";
        else if (standard == "2") poszukiwanaNazwa = "Premium";
        else if (standard == "3") poszukiwanaNazwa = "Deluxe";
        else
        {
            Console.WriteLine("Nieprawidłowy wybór standardu.");
            return;
        }

        Parcela znalezionaParcela = null;

        foreach (var p in parcele)
        {
            if (p.Nazwa == poszukiwanaNazwa && dlugosc <= p.MaxDlugoscPojazdu)
            {
                bool dostepna = true;
                foreach (var r in rezerwacje)
                {
                    if (r.ZarezerwowanaParcela.Numer == p.Numer)
                    {
                        if (przyjazd < r.DataOdjazdu && r.DataPrzyjazdu < odjazd)
                        {
                            dostepna = false;
                            break;
                        }
                    }
                }

                if (dostepna)
                {
                    znalezionaParcela = p;
                    break;
                }
            }
        }

        if (znalezionaParcela != null)
        {
            Rezerwacja nowaRezerwacja = new Rezerwacja(nastepneIdRezerwacji, znalezionaParcela, przyjazd, odjazd);
            rezerwacje.Add(nowaRezerwacja);
            Console.WriteLine($"\nSukces! Zarezerwowano parcelę nr {znalezionaParcela.Numer}. Twój numer rezerwacji to: {nastepneIdRezerwacji}");
            nastepneIdRezerwacji++;
        }
        else
        {
            if (dlugosc > parcele.Find(p => p.Nazwa == poszukiwanaNazwa).MaxDlugoscPojazdu)
            {
                Console.WriteLine("\nTwój pojazd jest za długi na ten standard parceli.");
            }
            else
            {
                Console.WriteLine("\nBrak dostępnego miejsca w tym terminie.");
            }
        }
    }

    public void UsunRezerwacje(int id)
    {
        Rezerwacja doUsuniecia = rezerwacje.Find(r => r.Id == id);
        if (doUsuniecia != null)
        {
            rezerwacje.Remove(doUsuniecia);
            Console.WriteLine("Rezerwacja została pomyślnie usunięta.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono rezerwacji o podanym numerze.");
        }
    }

    public void ZapiszDoPliku()
    {
        try
        {
            string folderUruchomieniowy = AppDomain.CurrentDomain.BaseDirectory;
            
            string sciezkaPliku = Path.GetFullPath(Path.Combine(folderUruchomieniowy, @"..\..\..\ListaRezerwacji.txt"));
            
            using (StreamWriter sw = new StreamWriter(sciezkaPliku))
            {
                sw.WriteLine("=== AKTUALNA LISTA REZERWACJI ===");
                if (rezerwacje.Count == 0)
                {
                    sw.WriteLine("Brak rezerwacji.");
                }
                else
                {
                    foreach (var r in rezerwacje)
                    {
                        sw.WriteLine($"ID: {r.Id} | Parcela nr: {r.ZarezerwowanaParcela.Numer} | Od: {r.DataPrzyjazdu:dd/MM/yyyy} Do: {r.DataOdjazdu:dd/MM/yyyy}");
                    }
                }
            }
            Console.WriteLine($"\nPomyślnie zapisano do pliku!");
            Console.WriteLine($"Lokalizacja: {sciezkaPliku}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nBłąd zapisu: " + ex.Message);
        }
    }
}
class Program
{
    static void Main()
    {
        SystemRezerwacji system = new SystemRezerwacji();
        bool dziala = true;

        while (dziala)
        {
            Console.WriteLine("\n--- SYSTEM REZERWACJI PARCELI ---");
            Console.WriteLine("1. Rezerwuj parcelę");
            Console.WriteLine("2. Usuń rezerwację");
            Console.WriteLine("3. Dane o parcelach");
            Console.WriteLine("4. Zapisz listę rezerwacji");
            Console.WriteLine("5. Wyjście z programu");
            Console.Write("Wybierz opcję: ");

            string wybor = Console.ReadLine();

            if (wybor == "1")
            {
                Console.WriteLine("\nStandard parceli: 1-Standard, 2-Premium, 3-Deluxe");
                Console.Write("Wybór: ");
                string std = Console.ReadLine();

                Console.Write("Długość pojazdu (m): ");
                if (!double.TryParse(Console.ReadLine(), out double dlugosc)) continue;

                Console.Write("Data przyjazdu (dd/MM/yyyy): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime przyjazd)) continue;

                Console.Write("Data odjazdu (dd/MM/yyyy): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime odjazd)) continue;

                if (przyjazd >= odjazd)
                {
                    Console.WriteLine("Błąd daty.");
                    continue;
                }

                system.Rezerwuj(std, dlugosc, przyjazd, odjazd);
            }
            else if (wybor == "2")
            {
                Console.Write("\nPodaj numer rezerwacji: ");
                if (int.TryParse(Console.ReadLine(), out int id)) system.UsunRezerwacje(id);
            }
            else if (wybor == "3") system.WyswietlParcele();
            else if (wybor == "4") system.ZapiszDoPliku();
            else if (wybor == "5") dziala = false;
        }
    }
}