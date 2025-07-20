namespace MultiNinja.Backend.Domain;

public abstract class DomainValue
{
    protected DomainValue(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public int Id { get; }

    public string Name { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not DomainValue cast)
        {
            return false;
        }

        if (!cast.GetType().IsInstanceOfType(this) && !this.GetType()!.IsInstanceOfType(cast))
        {
            return false;
        }

        if (cast.GetHashCode() != this.GetHashCode())
        {
            return false;
        }

        if (cast.Id != this.Id)
        {
            return false;
        }

        if (cast.Name != this.Name)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id, this.Name);
    }
}