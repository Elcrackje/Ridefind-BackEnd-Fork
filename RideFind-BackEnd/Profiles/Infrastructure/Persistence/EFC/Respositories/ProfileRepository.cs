using RideFind_BackEnd.Shared.Infraestructure.Persistence.EFC.Repositories;
using RideFind_BackEnd.Shared.Interfaces.ASP.Configuration;
using Microsoft.EntityFrameworkCore;

namespace RideFind_BackEnd.Profiles;

public class ProfileRepository(AppDbContext context) :
    BaseRepository<Profile>(context), IProfileRepository
{
    public Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().Where(p => p.Email == email)
            .FirstOrDefaultAsync();
    }
}