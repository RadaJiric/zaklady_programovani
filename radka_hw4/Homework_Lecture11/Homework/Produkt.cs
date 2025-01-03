
namespace Homework_Lecture11
{

    public class Product
    {
        public string Name;
        public decimal Price;
        public int Quantity;

        public Product() { }

        public Product(string name, decimal price, int quantity)
        {
            if (price < 0)
                throw new InvalidProductException("Price cannot be negative.");
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }


    public class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message) { }
    }

}