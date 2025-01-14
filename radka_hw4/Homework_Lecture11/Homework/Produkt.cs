
namespace Homework_Lecture11
{

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }



        public Product(string name, decimal price, int quantity)
        {
            if (price < 0)
                throw new InvalidProductException("Price cannot be negative.");
           
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }


}