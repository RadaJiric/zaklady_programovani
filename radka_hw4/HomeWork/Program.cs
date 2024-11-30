namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)

        {
            for (int i = 0; i < 2; i++)
            {


                Console.WriteLine("zadejte jméno a příjmení:");
                string? jmeno = Console.ReadLine();

                Console.WriteLine("zadejte vek:");
                string? vek = Console.ReadLine();

                Console.WriteLine("zadejte vahu v kilogramech:");
                string? vaha = Console.ReadLine();

                Console.WriteLine("zadejte výšku v centimetrech:");
                string? vyska = Console.ReadLine();

                {


                    Console.WriteLine(jmeno + "je starý(a) " + vek + " let. Važí " + vaha + " kilogramů a je " + vyska + " cm vysoky/a ");
                }

                int vaha2 = int.Parse(vaha);
                int vyska2 = int.Parse(vyska);
                double BMI = vaha2 / ((vyska2 * 0.01) * (vyska2 * 0.01));

                Console.WriteLine("vaše BMI je " + BMI);
            }

        }
    }
}
