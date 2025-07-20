namespace MultiNinja.Backend.Domain;

public sealed class EntityType : DomainValue
{
    public static readonly EntityType User = new EntityType(1, nameof(User));
    
    public EntityType(int id, string name)
        : base(id, name)
    {
    }
}