namespace Konwerter_Temperatur
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz, na jakie jednostki chcesz przeliczyć temperaturę (C lub F): ");
            string jednostka = Console.ReadLine().ToUpper(); //dajemy ToUpper gdyby użytkownik podał małą literę

            Console.WriteLine("Podaj wartość temperatury: ");
            var temperatura = int.Parse(Console.ReadLine());

            //przeliczniki
            var temp_F = temperatura * 1.8 + 32;
            var temp_C = (temperatura - 32) / 1.8;
            if (jednostka == "F"){
                Console.WriteLine($"Temperatura w stopniach Celsjusza wynosi: {temp_C}");
            }
            else if (jednostka == "C")
            {
                Console.WriteLine($"Temperatura w stopniach Farenheita wynosi: {temp_F}");
            }
            else
            {
                Console.WriteLine("Nie rozpoznano jednostki docelowej. Użyj C lub F");
            }
        }
    }
}