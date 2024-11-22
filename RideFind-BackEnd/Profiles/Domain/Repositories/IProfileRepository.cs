using RideFind_BackEnd.Shared.Domain.Repositories;

namespace RideFind_BackEnd.Profiles;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email);
}