using MultiNinja.Backend.Domain.Users;
using Shouldly;
using TestStack.BDDfy;

namespace MultiNinja.Backend.Domain.UnitTests.Users;

public class UserCreatedTests
{
    private Guid streamId;

    private EntityType entityType = null!;

    private Guid userId;

    private DateTime storageDate;

    private string displayName = null!;

    private UserCreated instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesHaveCorrectValues()
    {
        var idOfStream = Guid.NewGuid();
        var idOfUser = Guid.NewGuid();
        this.Given(t => t.StreamIdIs(idOfStream))
            .And(t => t.EntityTypeIs(new EntityType(4132, "dsginsdogis")))
            .And(t => t.UserIdIs(idOfUser))
            .And(t => t.StorageDateIs(new DateTime(2000, 1, 1)))
            .And(t => t.DisplayNameIs("aofpsamofpa"))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.StreamIdValueIs(idOfStream))
            .And(t => t.EntityTypeValueIs(new EntityType(4132, "dsginsdogis")))
            .And(t => t.UserIdValueIs(idOfUser))
            .And(t => t.StorageDateValueIs(new DateTime(2000, 1, 1)))
            .And(t => t.DisplayNameValueIs("aofpsamofpa"))
            .BDDfy();
    }
    
    private void StreamIdIs(Guid value) => this.streamId = value;
    
    private void EntityTypeIs(EntityType value) => this.entityType = value;
    
    private void UserIdIs(Guid value) => this.userId = value;
    
    private  void StorageDateIs(DateTime value) => this.storageDate = value;
    
    private void DisplayNameIs(string value) => this.displayName = value;

    private void InstanceIsCreated()
    {
        this.instance = UserCreated.Create(
            this.streamId,
            this.entityType,
            this.userId,
            this.storageDate,
            this.displayName);
    }

    private void StreamIdValueIs(Guid value)
    {
        this.instance.StreamId.ShouldBe(value);
    }

    private void EntityTypeValueIs(EntityType value)
    {
        this.instance.EntityType.ShouldBe(value);
    }

    private void UserIdValueIs(Guid value)
    {
        this.instance.UserId.ShouldBe(value);
    }

    private void StorageDateValueIs(DateTime value)
    {
        this.instance.StorageDate.ShouldBe(value);
    }

    private void DisplayNameValueIs(string value)
    {
        this.instance.DisplayName.ShouldBe(value);
    }
}