using System.Runtime.Serialization;

namespace DuckSales.Domains.Products.Exceptions;

[Serializable]
public class ProductsDomainException : Exception
{
    public ProductsDomainException()
    {
    }

    protected ProductsDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ProductsDomainException(string? message) : base(message)
    {
    }

    public ProductsDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
