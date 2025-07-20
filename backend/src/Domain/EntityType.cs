namespace MultiNinja.Backend.Domain;

public sealed class EntityType : DomainValue
{
    public static readonly EntityType User = new(1, nameof(User));
    
    public static readonly EntityType Credentials = new(2,  nameof(Credentials));
    
    public EntityType(int id, string name)
        : base(id, name)
    {
    }
}