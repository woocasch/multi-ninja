namespace MultiNinja.Backend.Application.ReadsRepository.Users;

public class GetUserByIdParameters
{
    public GetUserByIdParameters(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }
}