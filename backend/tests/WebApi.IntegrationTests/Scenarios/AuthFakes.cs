using Bogus;
using Bogus.DataSets;
using MultiNinja.Backend.WebApi.Endpoints.AuthModels;

namespace MultiNinja.Backend.WebApi.IntegrationTests.Scenarios;

public static class AuthFakes
{
    private static readonly Lazy<Faker<CreateAccountInput>> CreateAccountInputFaker = new(GenerateCreateAccountInputFaker);
    
    public static CreateAccountInput GetCreateAccountInput()
    {
        return CreateAccountInputFaker.Value.Generate();
    }

    private static Faker<CreateAccountInput> GenerateCreateAccountInputFaker()
    {
        var result = new Faker<CreateAccountInput>()
            .CustomInstantiator(f =>
                new CreateAccountInput(
                    f.Person.Email,
                    f.Internet.Password(),
                    f.Person.FullName));
        return result;
    }

    private static string Password(this Internet internet)
    {
        var r = internet.Random;

        var lowercase = r.Char('a', 'z').ToString();
        var uppercase = r.Char('A', 'Z').ToString();
        var number    = r.Char('0', '9').ToString();
        var symbol    = r.Char('!', '/').ToString();
        var padding   = r.String2(5);
        var padding2  = r.String2(r.Number(4, 8));  // random extra padding between min and max

        var chars = (lowercase + uppercase + number + symbol + padding + padding2).ToArray();
        var shuffledChars = r.Shuffle(chars).ToArray();

        return new string(shuffledChars);
    }
}