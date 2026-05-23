
class Payment:
    def __init__(self, miesiac, splata_kapitalu, splata_odsetek, pozostale_saldo):
        self.miesiac = miesiac
        self.splata_kapitalu = splata_kapitalu
        self.splata_odsetek = splata_odsetek
        self.pozostale_saldo = pozostale_saldo

class Loan:
    def __init__(self, kwota_kredytu, roczne_oprocentowanie, liczba_miesiecy):
        self.kwota_kredytu = kwota_kredytu
        self.roczne_oprocentowanie = roczne_oprocentowanie
        self.liczba_miesiecy = liczba_miesiecy

    def oblicz_miesieczna_rate(self):
        miesieczne_oprocentowanie = self.roczne_oprocentowanie / 12 / 100
        if miesieczne_oprocentowanie == 0:
            return self.kwota_kredytu / self.liczba_miesiecy
            
        return self.kwota_kredytu * (miesieczne_oprocentowanie * (1 + miesieczne_oprocentowanie)**self.liczba_miesiecy) / ((1 + miesieczne_oprocentowanie)**self.liczba_miesiecy - 1)

class AmortizationSchedule:
    def __init__(self, kredyt):
        self.kredyt = kredyt
        self.raty = []
        self.suma_odsetek = 0.0

    def generuj_harmonogram(self):
        miesieczne_oprocentowanie = self.kredyt.roczne_oprocentowanie / 12 / 100
        miesieczna_rata = self.kredyt.oblicz_miesieczna_rate()
        saldo = self.kredyt.kwota_kredytu

        for miesiac in range(1, self.kredyt.liczba_miesiecy + 1):
            odsetki = saldo * miesieczne_oprocentowanie
            kapital = miesieczna_rata - odsetki
            saldo -= kapital

            if saldo < 0.01:
                saldo = 0.0

            self.suma_odsetek += odsetki
            self.raty.append(Payment(miesiac, kapital, odsetki, saldo))

    def wypisz_harmonogram(self):
        print("\n--- HARMONOGRAM SPLAT ---")
        print(f"{'Miesiac':<8} | {'Kapital':<10} | {'Odsetki':<10} | {'Saldo':<10}")
        print("-" * 45)
        for rata in self.raty:
            print(f"{rata.miesiac:<8} | {rata.splata_kapitalu:<10.2f} | {rata.splata_odsetek:<10.2f} | {rata.pozostale_saldo:<10.2f}")
        print("-" * 45)
        print(f"Calkowite odsetki: {self.suma_odsetek:.2f}")

print("Witaj w kalkulatorze kredytowym!")
kwota = float(input("Podaj kwote kredytu (np. 10000): "))
oprocentowanie = float(input("Podaj roczne oprocentowanie w % (np. 5.0): "))
miesiace = int(input("Podaj liczbe miesiecy do splaty (np. 12): "))

moj_kredyt = Loan(kwota, oprocentowanie, miesiace)
harmonogram = AmortizationSchedule(moj_kredyt)

harmonogram.generuj_harmonogram()
harmonogram.wypisz_harmonogram()