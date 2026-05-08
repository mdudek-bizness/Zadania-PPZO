namespace Kalkulator_2_liczby.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w aplikacji Kalkulator 2 liczb!");
            Console.WriteLine("Podaj 1 liczbę:");
            //Parsujemy Console.ReadLine na int, bo readLine zwraca strigna
            var liczba1 = int.Parse(Console.ReadLine());


            Console.WriteLine("Podaj 2 liczbę:");
            //To samo z 2 liczbą - parsujemy na int
            var liczba2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj działanie do wykonania: +, -, *, /");
            var operator_arytmetyczny = Console.ReadLine();

            var wynik = 0;

            switch (operator_arytmetyczny)
            {
                case "+":
                    wynik = liczba1 + liczba2;
                    break;
                case "-":
                    wynik = liczba1 - liczba2;
                    break;
                case "*":
                    wynik = liczba1 * liczba2;
                    break;
                case "/":
                    wynik = liczba1 / liczba2;
                    break;
                default:
                    //Jeżeli użytkownik wpisze coś innego lub nic, wyświetlamy wyjątek
                    throw new Exception("Wybrałeś niepoprawną operację/nie wybrałeś operacji!");
            }
            //$ w Consloe.WriteLine pozwala na wstawianie zmiennych bez "+", tak wygodniej :)
            Console.WriteLine($"Wynik: {wynik}");
        }
    }
}