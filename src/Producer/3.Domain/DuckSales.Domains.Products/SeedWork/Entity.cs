namespace DuckSales.Domains.Products.SeedWork;

public abstract class Entity
{
    private Guid _id;
    private DateTime _createdIn;
    private DateTime? _updatedIn;

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

    public virtual DateTime CreatedIn
    {
        get
        {
            return _createdIn;
        }
        protected set
        {
            _createdIn = value;
        }
    }

    public virtual DateTime? UpdatedIn
    {
        get
        {
            return _updatedIn;
        }
        protected set
        {
            _updatedIn = value;
        }
    }
}
