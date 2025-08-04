using MultiNinja.Backend.Domain.Users;
using Shouldly;
using TestStack.BDDfy;

namespace MultiNinja.Backend.Domain.UnitTests.Users;

public sealed class UserEntityTests
{
    private Guid userId;

    private string displayName = null!;
    
    private UserEntity entity = null!;

    [Fact]
    public void WhenUserIsCreatedThenCorrectStateIsSet()
    {
        this.Given(t => t.UserIdIs(Guid.NewGuid()))
            .And(t => t.DisplayNameIs("Display Name"))
            .When(t => t.EntityIsCreated(this.userId, this.displayName))
            .Then(t => t.EntityIsNotNull())
            .And(t => t.UserIdValueIs(this.userId))
            .And(t => t.DisplayNameValueIs(this.displayName))
            .And(t => t.EntityContainsEvent(ee => ee is UserCreated, $"missing '{nameof(UserCreated)}' event"))
            .BDDfy();
    }

    private void DisplayNameIs(string value)
    {
        this.displayName = value;
    }

    private void UserIdIs(Guid value)
    {
        this.userId = value;
    }

    private void EntityIsCreated(Guid userId, string displayName)
    {
        this.entity = UserEntity.Create(userId, displayName);
    }

    private void EntityIsNotNull()
    {
        this.entity.ShouldNotBeNull();
    }

    private void DisplayNameValueIs(string value)
    {
        this.entity.DisplayName.ShouldBe(value);
    }

    private void UserIdValueIs(Guid value)
    {
        this.entity.UserId.ShouldBe(value);
    }

    private void EntityContainsEvent(Func<EntityEvent, bool> predicate, string message)
    {
        this.entity.Events.ShouldContain(e => predicate(e), message);
    }
}