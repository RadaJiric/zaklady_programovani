

using Homework_Lecture11;
using System.Text.Json;

namespace Homework_Lecture11
{
    
        public class Catalog
        {
            public List<Product> Products { get; private set; } = new List<Product>();


            public void AddProduct(Product product)
            {
            if (product != null)
            {
                Products.Add(product);
            }
        }


            public List<string> SerializeProducts()
            {
                var jsonStrings = new List<string>();
                foreach (var product in Products)
                {
                    jsonStrings.Add(JsonSerializer.Serialize(product));
                }
                return jsonStrings;
            }


            public void DeserializeProducts(List<string> jsonStrings)
            {
                foreach (var jsonString in jsonStrings)
                {
                    try
                    {
                        var product = JsonSerializer.Deserialize<Product>(jsonString);
                        AddProduct(product);
                }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Deserialization error: {ex.Message}");
                    }
                    catch (InvalidProductException ex)
                    {
                        Console.WriteLine($"Invalid product error: {ex.Message}");
                    }
                }
            }
        }
    
}
