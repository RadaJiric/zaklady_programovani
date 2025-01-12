using Homework_Lecture11;

namespace Homework_Lecture11
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var catalog = new Catalog();

          
            catalog.AddProduct(new Product("Laptop", 1500, 10));
            catalog.AddProduct(new Product("Phone", 800, 20));
            try
            {
                catalog.AddProduct(new Product("Tablet", -500, 5));
            }
            catch (InvalidProductException ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}");
            }

          
            var serializedProducts = catalog.SerializeProducts();
            Console.WriteLine("Serialized products:");
            foreach (var json in serializedProducts)
            {
                Console.WriteLine(json);
            }

         
            var manualJson = "{\"Name\":\"Monitor\",\"Price\":300,\"Quantity\":15}";
            var invalidJson = "{\"Name\":\"Keyboard\",\"Price\":\"invalid_price\",\"Quantity\":5}";

         
            catalog.DeserializeProducts(new List<string> { manualJson, invalidJson });

         
            Console.WriteLine("\nProducts after deserialization:");
            foreach (var product in catalog.Products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }

            Console.WriteLine("\nProgram finished without errors.");
        }
    }
}