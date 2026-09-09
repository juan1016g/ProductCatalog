namespace Domain.Exceptions
{
    public class InvalidStockException : Exception
    {
        public InvalidStockException(string message) : base(message)
        {
        }
    }
}
