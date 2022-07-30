using MediatR;

namespace DuckSales.Domains.Products.SeedWork;

public abstract class Entity
{
    private Guid _id;

    public virtual Guid Id
    {
        get
        {
            return _id;
        }
        protected set
        {
            _id = value;
        }
    }
}
