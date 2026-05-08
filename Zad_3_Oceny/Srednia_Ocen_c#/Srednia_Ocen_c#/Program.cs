namespace Srednia_Ocen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kalkulator średniej ocen ucznia");

            Console.WriteLine("Podaj liczbę ocen: ");
            int iloscOcen = int.Parse(Console.ReadLine());

            //double w przypadku, gdyby liczby były zmiennoprzecinkowe
            double sumaOcen = 0;

            for (int i = 1; i <= iloscOcen; i++)
            {
                Console.Write("Podaj ocenę nr " + i + ": ");
                double ocena = double.Parse(Console.ReadLine());
                sumaOcen = sumaOcen + ocena;
            }

            double srednia = sumaOcen / iloscOcen;

            //zaokraglenie do 2 miejsc pop rzecinku
            Console.WriteLine("Średnia ocen: " + srednia.ToString("F1"));

            //warunek zaliczenia przedmiotu >=3.0
            if (srednia >= 3.0)
            {
                Console.WriteLine("Uczeń zdał.");
            }
            else
            {
                Console.WriteLine("Uczeń nie zdał.");
            }
        }
    }
}