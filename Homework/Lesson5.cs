namespace Homework
{
    public class Lesson5
    {
        
        
            public static void Run()
            {
                Console.WriteLine("Zadejte své údaje:");

                
                string jmeno = ZiskejText("Jméno");
                string prijmeni = ZiskejText("Příjmení");
                int vek = ZiskejCislo("Věk");
                double vaha = ZiskejCisloDouble("Váha (kg)");
                double vyska = ZiskejCisloDouble("Výška (cm)");

               
                double bmi = VypocetBMI(vaha, vyska);

          
                string kategorie = KategorieBMI(bmi);

                
                Console.WriteLine($"\n{jmeno} {prijmeni} je starý(á) {vek} let. Váží {vaha} kilogramů a je {vyska} centimetrů vysoký(á).");
                Console.WriteLine($"Jeho/její BMI je {bmi:F2} a spadá do kategorie: {kategorie}.");
            }

            public static string ZiskejText(string nazev)
            {
                Console.Write($"Zadejte {nazev}: ");
                return Console.ReadLine();
            }

            public static int ZiskejCislo(string nazev)
            {
                while (true)
                {
                    Console.Write($"Zadejte {nazev}: ");
                    if (int.TryParse(Console.ReadLine(), out int cislo))
                    {
                        return cislo;
                    }
                    Console.WriteLine("Neplatný vstup, zadejte celé číslo.");
                }
            }

            public static double ZiskejCisloDouble(string nazev)
            {
                while (true)
                {
                    Console.Write($"Zadejte {nazev}: ");
                    if (double.TryParse(Console.ReadLine(), out double cislo))
                    {
                        return cislo;
                    }
                    Console.WriteLine("Neplatný vstup, zadejte číslo (např. 70.5).");
                }
            }

            public static double VypocetBMI(double vaha, double vyska)
            {
                double vyskaVM = vyska / 100.0;
                return vaha / (vyskaVM * vyskaVM);
            }

            public static string KategorieBMI(double bmi)
            {
                if (bmi < 18.5)
                    return "Podváha";
                else if (bmi >= 18.5 && bmi < 25)
                    return "Normální váha";
                else if (bmi >= 25 && bmi < 30)
                    return "Nadváha";
                else
                    return "Obezita";
            }
        
    }
}