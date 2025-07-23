using System.Collections.ObjectModel;

namespace MultiNinja.Backend.Domain;

public sealed class EntityType : DomainValue
{
    public static readonly EntityType User = new(1, nameof(User));
    
    public static readonly EntityType Credentials = new(2,  nameof(Credentials));

    private static readonly ReadOnlyCollection<EntityType> entityTypes = new([User, Credentials]);
    
    public EntityType(int id, string name)
        : base(id, name)
    {
    }

    public static EntityType? FromName(string name)
    {
        return entityTypes.SingleOrDefault(et => et.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}