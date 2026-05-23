using System;
using System.Collections.Generic;

public class Payment
{
    public int Miesiac { get; set; }
    public double SplataKapitalu { get; set; }
    public double SplataOdsetek { get; set; }
    public double PozostaleSaldo { get; set; }

    public Payment(int miesiac, double splataKapitalu, double splataOdsetek, double pozostaleSaldo)
    {
        Miesiac = miesiac;
        SplataKapitalu = splataKapitalu;
        SplataOdsetek = splataOdsetek;
        PozostaleSaldo = pozostaleSaldo;
    }
}

public class Loan
{
    public double KwotaKredytu { get; set; }
    public double RoczneOprocentowanie { get; set; }
    public int LiczbaMiesiecy { get; set; }

    public Loan(double kwotaKredytu, double roczneOprocentowanie, int liczbaMiesiecy)
    {
        KwotaKredytu = kwotaKredytu;
        RoczneOprocentowanie = roczneOprocentowanie;
        LiczbaMiesiecy = liczbaMiesiecy;
    }

    public double ObliczMiesiecznaRate()
    {
        double miesieczneOprocentowanie = RoczneOprocentowanie / 12 / 100;
        if (miesieczneOprocentowanie == 0) return KwotaKredytu / LiczbaMiesiecy;

        return KwotaKredytu * (miesieczneOprocentowanie * Math.Pow(1 + miesieczneOprocentowanie, LiczbaMiesiecy)) / (Math.Pow(1 + miesieczneOprocentowanie, LiczbaMiesiecy) - 1);
    }
}

public class AmortizationSchedule
{
    public Loan SzczegolyKredytu { get; set; }
    public List<Payment> Raty { get; set; }
    public double SumaOdsetek { get; set; }

    public AmortizationSchedule(Loan kredyt)
    {
        SzczegolyKredytu = kredyt;
        Raty = new List<Payment>();
        SumaOdsetek = 0;
    }

    public void GenerujHarmonogram()
    {
        double miesieczneOprocentowanie = SzczegolyKredytu.RoczneOprocentowanie / 12 / 100;
        double miesiecznaRata = SzczegolyKredytu.ObliczMiesiecznaRate();
        double saldo = SzczegolyKredytu.KwotaKredytu;

        for (int miesiac = 1; miesiac <= SzczegolyKredytu.LiczbaMiesiecy; miesiac++)
        {
            double odsetki = saldo * miesieczneOprocentowanie;
            double kapital = miesiecznaRata - odsetki;
            saldo -= kapital;

            if (saldo < 0.01) saldo = 0;

            SumaOdsetek += odsetki;
            Raty.Add(new Payment(miesiac, kapital, odsetki, saldo));
        }
    }

    public void WypiszHarmonogram()
    {
        Console.WriteLine("\n--- HARMONOGRAM SPŁAT ---");
        Console.WriteLine($"{"Miesiąc",-8} | {"Kapitał",-10} | {"Odsetki",-10} | {"Saldo",-10}");
        Console.WriteLine(new string('-', 45));

        foreach (var rata in Raty)
        {
            Console.WriteLine($"{rata.Miesiac,-8} | {rata.SplataKapitalu,-10:F2} | {rata.SplataOdsetek,-10:F2} | {rata.PozostaleSaldo,-10:F2}");
        }

        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"Całkowite odsetki: {SumaOdsetek:F2}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Witaj w kalkulatorze kredytowym!");

        Console.Write("Podaj kwotę kredytu: ");
        double kwota = Convert.ToDouble(Console.ReadLine());

        Console.Write("Podaj roczne oprocentowanie w %: ");
        double oprocentowanie = Convert.ToDouble(Console.ReadLine());

        Console.Write("Podaj liczbę miesięcy do spłaty: ");
        int miesiace = Convert.ToInt32(Console.ReadLine());

        Loan mojKredyt = new Loan(kwota, oprocentowanie, miesiace);
        AmortizationSchedule harmonogram = new AmortizationSchedule(mojKredyt);

        harmonogram.GenerujHarmonogram();
        harmonogram.WypiszHarmonogram();
    }
}