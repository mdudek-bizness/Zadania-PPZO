# System Rezerwacji Parceli Kempingowej

Aplikacja konsolowa służąca do zarządzania rezerwacjami miejsc na polu kempingowym. Program pozwala użytkownikowi wybrać standard parceli (Standard, Premium, Deluxe), weryfikuje dopuszczalną długość pojazdu kempingowego oraz sprawdza dostępność miejsc w zadanym przedziale czasowym (zabezpieczenie przed nakładaniem się dat). Posiada również możliwość usunięcia rezerwacji oraz eksportu listy rezerwacji do pliku tekstowego.

# Lista klas
* **Parcela (Abstrakcyjna)** - Odpowiada za przechowywanie ogólnych cech działki.
  * Właściwości: `Numer`, `Nazwa`, `MaxDlugoscPojazdu`, `CenaZaDobe`
  * Metody: `PokazOpis()` (abstrakcyjna)
* **ParcelaStandard, ParcelaPremium, ParcelaDeluxe** - Klasy szczegółowe ustalające specyficzne wyposażenie, wymiary i ceny parceli za pomocą konstruktora.
* **Rezerwacja** - Odpowiada za przechowanie pojedynczego wpisu rezerwacyjnego.
  * Właściwości: `Id`, `ZarezerwowanaParcela`, `DataPrzyjazdu`, `DataOdjazdu`
  * Metody: `WypiszSzczegoly()`
* **SystemRezerwacji** - Odpowiada za zarządzanie logiką: przydziela parcele, kontroluje daty, usuwa wpisy i zapisuje dane do pliku.
  * Właściwości (pola ukryte): Kolekcje obiektów `parcele` i `rezerwacje`
  * Metody: `Rezerwuj()`, `UsunRezerwacje()`, `WyswietlParcele()`, `ZapiszDoPliku()`
* **Program** - Klasa startowa odpowiadająca wyłącznie za interfejs użytkownika w konsoli (menu).

## Opis relacji między klasami
* **Agregacja / Właściwość:** Klasa `Rezerwacja` przyjmuje w konstruktorze i przechowuje jako właściwość pełny obiekt klasy `Parcela` (Rezerwacja *posiada* Parcelę).
* **Kolekcja obiektów:** Klasa `SystemRezerwacji` przechowuje listy powiązanych obiektów: `List<Parcela>` oraz `List<Rezerwacja>`.
* **Dziedziczenie:** Klasy `ParcelaStandard`, `ParcelaPremium` i `ParcelaDeluxe` dziedziczą bezpośrednio z bazowej klasy `Parcela`.

## Wskazanie czterech zasad OOP
1. **Enkapsulacja:** Pola w klasie `SystemRezerwacji` (listy parceli i rezerwacji) są ustawione jako `private`, co uniemożliwia ich niekontrolowaną zmianę z zewnątrz. Właściwości obiektów (np. `Id` w `Rezerwacja`) mają prywatne settery (`get; private set;`).
2. **Dziedziczenie:** Klasy `ParcelaStandard`, `ParcelaPremium` i `ParcelaDeluxe` rozszerzają jedną, bazową klasę `Parcela`, dziedzicząc z niej podstawowe właściwości (takie jak Numer czy Cena) i omijając konieczność powielania kodu.
3. **Abstrakcja:** Klasa `Parcela` posiada modyfikator `abstract`, przez co opisuje ogólny zarys tego, jak ma wyglądać miejsce kempingowe, nie pozwalając na stworzenie "pustej, ogólnej parceli" (nie można stworzyć instancji klasy abstrakcyjnej). Wymusza również kontrakt implementacji metody `PokazOpis()`.
4. **Polimorfizm:** Metoda `WyswietlParcele()` w klasie `SystemRezerwacji` wywołuje w pętli/na indeksach tę samą metodę `PokazOpis()`. Ponieważ każdy z obiektów dziedziczy po innej klasie (Standard, Premium, Deluxe), podział obowiązków sprawia, że program wykonuje właściwą (nadpisaną) operację dla danego typu obiektu.    
